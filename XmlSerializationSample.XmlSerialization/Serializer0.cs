// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

  using Microsoft.IO;

  public sealed class Serializer0 : ISerializer
  {
    private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

    public Serializer0(RecyclableMemoryStreamManager recyclableMemoryStreamManager)
    {
      _recyclableMemoryStreamManager = recyclableMemoryStreamManager ?? throw new ArgumentNullException(nameof(recyclableMemoryStreamManager));
    }

    public Task<string> SerializeAsync(object document, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task SerializeAsync(object document, Stream stream, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<object> DeserializeAsync(string input, Type type, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<object> DeserializeAsync(Stream input, Type type, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<TDocument> DeserializeAsync<TDocument>(string input, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task<TDocument> DeserializeAsync<TDocument>(Stream input, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
  }
}
}
