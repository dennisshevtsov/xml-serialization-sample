// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.IO;
  using System.Text;
  using System.Threading.Tasks;
  using System.Xml.Serialization;

  using Microsoft.IO;

  public sealed class Serializer
  {
    private readonly RecyclableMemoryStreamManager _streamManager;

    public Serializer(RecyclableMemoryStreamManager streamManager)
      => _streamManager = streamManager ?? throw new ArgumentNullException(nameof(streamManager));

    public Task<string> SerializeAsync<TDocument>(TDocument document)
      where TDocument : ProductXmlDocumentBase
    {
      using (var stream = _streamManager.GetStream())
      {
        using (Serialize(document, stream))
        {
          return ReadAsync(stream);
        }
      }
    }

    private IDisposable Serialize<TDocument>(TDocument document, Stream stream)
    {
      var writer = new StreamWriter(stream, Encoding.UTF8);

      Serialize(document, writer);

      stream.Seek(0, SeekOrigin.Begin);

      return writer;
    }

    private void Serialize<TDocument>(TDocument document, StreamWriter writer)
    {
      var xmlSerializer = new XmlSerializer(
        typeof(ProductXmlDocumentBase),
        new[]
        {
          typeof(TDocument),
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

    public TDocument Deserialize<TDocument>(Stream stream)
      where TDocument : ProductXmlDocumentBase
    {
      using (var reader = new StreamReader(stream, Encoding.UTF8))
      {
        var serializer = new XmlSerializer(
          typeof(ProductXmlDocumentBase),
          new[]
          {
            typeof(TDocument),
          });

        var namespaces = new XmlSerializerNamespaces();
        namespaces.Add("", "");

        return serializer.Deserialize(reader) as TDocument;
      }
    }

    public TDocument Deserialize<TDocument>(string document)
      where TDocument : ProductXmlDocumentBase
    {
      using (var stream = _streamManager.GetStream())
      {
        var writer = new StreamWriter(stream, Encoding.UTF8);
        {
          writer.WriteLine(document);
          writer.Flush();
          stream.Seek(0, SeekOrigin.Begin);

          using (var reader = new StreamReader(stream, Encoding.UTF8))
          {
            var serializer = new XmlSerializer(
              typeof(TDocument),
              new XmlRootAttribute("product"));

            var r = serializer.Deserialize(reader) as TDocument;

            return r;
          }
        }
      }
    }
  }
}
