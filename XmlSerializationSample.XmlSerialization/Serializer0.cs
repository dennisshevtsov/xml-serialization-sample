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

  public sealed class Serializer0 : ISerializer
  {
    private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

    public Serializer0(RecyclableMemoryStreamManager recyclableMemoryStreamManager)
      => _recyclableMemoryStreamManager = recyclableMemoryStreamManager ?? throw new ArgumentNullException(nameof(recyclableMemoryStreamManager));

    public Task<string> SerializeAsync(object document, CancellationToken cancellationToken)
    {
      using (var stream = _recyclableMemoryStreamManager.GetStream()) 
      using (var writer = new StreamWriter(stream, Encoding.UTF8))
      {
        Serialize(document, writer);
        stream.Seek(0, SeekOrigin.Begin);

        return ReadAsync(stream);
      }
    }

    private void Serialize(object document, StreamWriter writer)
    {
      var xmlSerializer = new XmlSerializer(
        typeof(ProductXmlDocumentBase),
        new[]
        {
          document.GetType(),
        });

      var namespaces = new XmlSerializerNamespaces();
      namespaces.Add("", "");

      xmlSerializer.Serialize(writer, document, namespaces);
    }

    private async Task<string> ReadAsync(Stream stream)
    {
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        return await reader.ReadToEndAsync();
      }
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
