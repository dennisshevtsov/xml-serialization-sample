// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  using System;
  using System.IO;
  using System.Text;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Xml.Linq;

  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public class SerializerTest
  {
    private const int RandomTokenSize = 5;

    private IDisposable _disposable;
    private ISerializer _serializer;

    [TestInitialize]
    public void Initialize()
    {
      var services = new ServiceCollection();

      services.AddSerialization(
        config => config.Add(new LaptopDocumentConfiguration()));

      var provider = services.BuildServiceProvider();

      _disposable = provider;
      _serializer = provider.GetRequiredService<ISerializer>();
    }

    [TestCleanup]
    public void Cleanup() => _disposable?.Dispose();

    [TestMethod]
    public async Task TestSerialize()
    {
      var document = SerializerTest.GenerateLaptop();
      var xml = await _serializer.SerializeAsync(document, CancellationToken.None);

      SerializerTest.Check(document, xml);
    }

    [TestMethod]
    public async Task TestSerializeStream()
    {
      using (var stream = new MemoryStream())
      {
        var document = SerializerTest.GenerateLaptop();

        await _serializer.SerializeAsync(document, stream, CancellationToken.None);

        using (var reader = new StreamReader(stream, Encoding.UTF8))
        {
          stream.Seek(0, SeekOrigin.Begin);

          var xml = await reader.ReadToEndAsync();

          SerializerTest.Check(document, xml);
        }
      }
    }

    [TestMethod]
    public async Task TestDeserialize()
    {
      var expected = SerializerTest.GenerateLaptop();
      var xml = SerializerTest.GenerateXml(expected);
      var document = await _serializer.DeserializeAsync(
        xml, typeof(LaptopDocument), CancellationToken.None);

      Assert.IsNotNull(document);

      var actual = document as LaptopDocument;

      SerializerTest.Check(expected, actual);
    }

    [TestMethod]
    public async Task TestDeserializeStream()
    {
      var expected = SerializerTest.GenerateLaptop();
      var xml = SerializerTest.GenerateXml(expected);

      using (var stream = new MemoryStream())
      using (var writer = new StreamWriter(stream))
      {
        await writer.WriteLineAsync(xml);
        await writer.FlushAsync();

        var document = await _serializer.DeserializeAsync(
          stream, typeof(LaptopDocument), CancellationToken.None);

        Assert.IsNotNull(document);

        var actual = document as LaptopDocument;

        SerializerTest.Check(expected, actual);
      }
    }

    [TestMethod]
    public async Task TestDeserializeGeneric()
    {
      var expected = SerializerTest.GenerateLaptop();
      var xml = SerializerTest.GenerateXml(expected);
      var actual = await _serializer.DeserializeAsync<LaptopDocument>(
        xml, CancellationToken.None);

      SerializerTest.Check(expected, actual);
    }

    [TestMethod]
    public async Task TestDeserializeGenericStream()
    {
      var expected = SerializerTest.GenerateLaptop();
      var xml = SerializerTest.GenerateXml(expected);
      using (var stream = new MemoryStream())
      using (var writer = new StreamWriter(stream))
      {
        await writer.WriteLineAsync(xml);
        await writer.FlushAsync();

        var actual = await _serializer.DeserializeAsync<LaptopDocument>(
          stream, CancellationToken.None);

        SerializerTest.Check(expected, actual);
      }
    }

    private static string GenerateToken(int length = SerializerTest.RandomTokenSize) =>
      Guid.NewGuid()
          .ToString()
          .Replace("-", "")
          .Substring(0, length)
          .ToUpper();

    private static LaptopDocument GenerateLaptop() =>
      new LaptopDocument
      {
        Sku = SerializerTest.GenerateToken(),
        Title = SerializerTest.GenerateToken(),
        Description = SerializerTest.GenerateToken(),
        ScreenSize = SerializerTest.GenerateToken(),
        Processor = SerializerTest.GenerateToken(),
        RamVolume = SerializerTest.GenerateToken(),
      };

    private static string GenerateXml(LaptopDocument document) =>
@$"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""{document.Sku}"">
  <title>{document.Title}</title>
  <description>{document.Description}</description>
  <screen-size>{document.ScreenSize}</screen-size>
  <processor>{document.Processor}</processor>
  <ram-volume>{document.RamVolume}</ram-volume>
</product>";

    private static void Check(LaptopDocument document, string xml)
    {
      Assert.IsNotNull(xml);

      var parsed = XDocument.Parse(xml);

      Assert.AreEqual("product", parsed.Root.Name.LocalName);
      Assert.AreEqual(document.Sku, parsed.Root.Attribute("sku").Value);
      Assert.AreEqual(document.Title, parsed.Root.Element("title").Value);
      Assert.AreEqual(document.Description, parsed.Root.Element("description").Value);
      Assert.AreEqual(document.ScreenSize, parsed.Root.Element("screen-size").Value);
      Assert.AreEqual(document.Processor, parsed.Root.Element("processor").Value);
      Assert.AreEqual(document.RamVolume, parsed.Root.Element("ram-volume").Value);
    }

    private static void Check(LaptopDocument expected, LaptopDocument actual)
    {
      Assert.IsNotNull(actual);

      Assert.AreEqual(expected.Sku, actual.Sku);
      Assert.AreEqual(expected.Title, actual.Title);
      Assert.AreEqual(expected.Description, actual.Description);
      Assert.AreEqual(expected.ScreenSize, actual.ScreenSize);
      Assert.AreEqual(expected.Processor, actual.Processor);
      Assert.AreEqual(expected.RamVolume, actual.RamVolume);
    }
  }
}
