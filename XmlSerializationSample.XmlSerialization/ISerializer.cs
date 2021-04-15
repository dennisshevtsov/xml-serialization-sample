// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

  public interface ISerializer
  {
    public Task<string> SerializeAsync(object document, CancellationToken cancellationToken);

    public Task SerializeAsync(object document, Stream stream, CancellationToken cancellationToken);

    public Task<object> DeserializeAsync(string input, Type type, CancellationToken cancellationToken);

    public Task<object> DeserializeAsync(Stream input, Type type, CancellationToken cancellationToken);

    public Task<TDocument> DeserializeAsync<TDocument>(string input, CancellationToken cancellationToken)
      where TDocument : class;

    public Task<TDocument> DeserializeAsync<TDocument>(Stream input, CancellationToken cancellationToken)
      where TDocument : class;
  }
}
