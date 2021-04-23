// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

  /// <summary>Provides a simple API to serialize/deserialize an object.</summary>
  public interface ISerializer
  {
    /// <summary>Serializes an object.</summary>
    /// <param name="document">An object that reprennts a document that is required to serialize.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<string> SerializeAsync(object document, CancellationToken cancellationToken);

    /// <summary>Serializes an object.</summary>
    /// <param name="document">An object that reprennts a document that is required to serialize.</param>
    /// <param name="stream"></param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task SerializeAsync(object document, Stream stream, CancellationToken cancellationToken);

    /// <summary>Deserializes an object.</summary>
    /// <param name="input">An object that represents a serialized object.</param>
    /// <param name="type">An object that represents a type of a serialized object.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<object> DeserializeAsync(string input, Type type, CancellationToken cancellationToken);

    /// <summary>Deserializes an object.</summary>
    /// <param name="input">An object that represents a serialized object.</param>
    /// <param name="type">An object that represents a type of a serialized object.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<object> DeserializeAsync(Stream input, Type type, CancellationToken cancellationToken);

    /// <summary>Deserializes an object.</summary>
    /// <typeparam name="TDocument">A type of a serialized object.</typeparam>
    /// <param name="input">An object that represents a serialized object.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TDocument> DeserializeAsync<TDocument>(string input, CancellationToken cancellationToken)
      where TDocument : class;

    /// <summary>Deserializes an object.</summary>
    /// <typeparam name="TDocument">A type of a serialized object.</typeparam>
    /// <param name="input">An object that represents a serialized object.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TDocument> DeserializeAsync<TDocument>(Stream input, CancellationToken cancellationToken)
      where TDocument : class;
  }
}
