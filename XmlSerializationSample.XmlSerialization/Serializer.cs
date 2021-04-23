// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.IO;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Xml.Serialization;

  using Microsoft.IO;

  /// <summary>Provides a simple API to serialize/deserialize an object.</summary>
  public sealed class Serializer : ISerializer
  {
    private readonly RecyclableMemoryStreamManager _streamManager;
    private readonly ISerializerProvider _serializerProvider;

    /// <summary>Initializes a new instance of the <see cref="XmlSerializationSample.XmlSerialization.Serializer"/> class.</summary>
    /// <param name="streamManager">An object that manages pools of Microsoft.IO.RecyclableMemoryStream objects.</param>
    /// <param name="serializerProvider">An object that provides a simple API to get an instance of the <see cref="System.Xml.Serialization.XmlSerializer"/> class for a type of a serializing object.</param>
    public Serializer(
      RecyclableMemoryStreamManager streamManager,
      ISerializerProvider serializerProvider)
    {
      _streamManager = streamManager ?? throw new ArgumentNullException(nameof(streamManager));
      _serializerProvider = serializerProvider ?? throw new ArgumentNullException(nameof(serializerProvider));
    }

    /// <summary>Serializes an object.</summary>
    /// <param name="document">An object that reprennts a document that is required to serialize.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<string> SerializeAsync(object document, CancellationToken cancellationToken)
    {
      using (var stream = _streamManager.GetStream())
      using (var writer = new StreamWriter(stream, Encoding.UTF8))
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        Serialize(document, writer);
        stream.Seek(0, SeekOrigin.Begin);

        return await reader.ReadToEndAsync();
      }
    }

    /// <summary>Serializes an object.</summary>
    /// <param name="document">An object that reprennts a document that is required to serialize.</param>
    /// <param name="stream"></param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public async Task SerializeAsync(object document, Stream output, CancellationToken cancellationToken)
    {
      using (var stream = _streamManager.GetStream())
      using (var writer = new StreamWriter(stream, Encoding.UTF8))
      {
        Serialize(document, writer);

        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(output, cancellationToken);
      }
    }

    /// <summary>Deserializes an object.</summary>
    /// <param name="input">An object that represents a serialized object.</param>
    /// <param name="type">An object that represents a type of a serialized object.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<object> DeserializeAsync(string input, Type type, CancellationToken cancellationToken)
    {
      using (var stream = _streamManager.GetStream())
      using (var writer = new StreamWriter(stream, Encoding.UTF8))
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        await writer.WriteLineAsync(input);
        await writer.FlushAsync();

        stream.Seek(0, SeekOrigin.Begin);

        var serializer = _serializerProvider.Get(type);
        var output = serializer.Deserialize(reader);

        return output;
      }
    }

    /// <summary>Deserializes an object.</summary>
    /// <param name="input">An object that represents a serialized object.</param>
    /// <param name="type">An object that represents a type of a serialized object.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<object> DeserializeAsync(Stream input, Type type, CancellationToken cancellationToken)
    {
      using (var reader = new StreamReader(input, Encoding.UTF8))
      {
        input.Seek(0, SeekOrigin.Begin);

        var serializer = _serializerProvider.Get(type);
        var output = serializer.Deserialize(reader);

        return Task.FromResult(output);
      }
    }

    /// <summary>Deserializes an object.</summary>
    /// <typeparam name="TDocument">A type of a serialized object.</typeparam>
    /// <param name="input">An object that represents a serialized object.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public async Task<TDocument> DeserializeAsync<TDocument>(string input, CancellationToken cancellationToken)
      where TDocument : class
    {
      using (var stream = _streamManager.GetStream())
      using (var writer = new StreamWriter(stream, Encoding.UTF8))
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        await writer.WriteLineAsync(input);
        await writer.FlushAsync();

        stream.Seek(0, SeekOrigin.Begin);

        var serializer = _serializerProvider.Get(typeof(TDocument));
        var output = serializer.Deserialize(reader) as TDocument;

        return output;
      }
    }

    /// <summary>Deserializes an object.</summary>
    /// <typeparam name="TDocument">A type of a serialized object.</typeparam>
    /// <param name="input">An object that represents a serialized object.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TDocument> DeserializeAsync<TDocument>(Stream stream, CancellationToken cancellationToken)
      where TDocument : class
    {
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        stream.Seek(0, SeekOrigin.Begin);

        var serializer = _serializerProvider.Get(typeof(TDocument));
        var output = serializer.Deserialize(reader) as TDocument;

        return Task.FromResult(output);
      }
    }

    private void Serialize(object document, StreamWriter writer)
    {
      var xmlSerializer = _serializerProvider.Get(document.GetType());

      var namespaces = new XmlSerializerNamespaces();
      namespaces.Add("", "");

      xmlSerializer.Serialize(writer, document, namespaces);
    }
  }
}
