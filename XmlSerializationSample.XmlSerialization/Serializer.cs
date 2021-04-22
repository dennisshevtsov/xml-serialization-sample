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

  public sealed class Serializer : ISerializer
  {
    private readonly RecyclableMemoryStreamManager _streamManager;
    private readonly ISerializerProvider _serializerProvider;

    public Serializer(
      RecyclableMemoryStreamManager streamManager,
      ISerializerProvider serializerProvider)
    {
      _streamManager = streamManager ?? throw new ArgumentNullException(nameof(streamManager));
      _serializerProvider = serializerProvider ?? throw new ArgumentNullException(nameof(serializerProvider));
    }

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

    private void Serialize(object document, StreamWriter writer)
    {
      var xmlSerializer = _serializerProvider.Get(document.GetType());

      var namespaces = new XmlSerializerNamespaces();
      namespaces.Add("", "");

      xmlSerializer.Serialize(writer, document, namespaces);
    }

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

    public Task<object> DeserializeAsync(Stream stream, Type type, CancellationToken cancellationToken)
    {
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        stream.Seek(0, SeekOrigin.Begin);

        var serializer = _serializerProvider.Get(type);
        var output = serializer.Deserialize(reader);

        return Task.FromResult(output);
      }
    }

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
  }
}
