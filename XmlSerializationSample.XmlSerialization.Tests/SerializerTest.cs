// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;
  using System.Xml.Serialization;

  using Microsoft.Extensions.DependencyInjection;
  using Microsoft.VisualStudio.TestTools.UnitTesting;

  [TestClass]
  public class SerializerTest
  {
    private IDisposable _disposable;
    private ISerializer _serializer;

    [TestInitialize]
    public void Initialize()
    {
      var services = new ServiceCollection();

      services.AddSerialization(
        config => config.Add(typeof(LaptopXmlDocument), new XmlAttributeOverrides())
                        .Add(typeof(OpticalMouseXmlDocument), new XmlAttributeOverrides()));

      var provider = services.BuildServiceProvider();

      _disposable = provider;
      _serializer = provider.GetRequiredService<ISerializer>();
    }

    [TestCleanup]
    public void Cleanup() => _disposable?.Dispose();

    [TestMethod]
    public async Task TestSerialize()
    {
      var laptopXmlDocument = new LaptopXmlDocument
      {
        Sku = "test",
        Title = "Test Test",
        Description = "Test test test.",
        ScreenSize = "test",
        Processor = "test",
        RamVolume = "test",
      };
      var laptopXml = await _serializer.SerializeAsync(laptopXmlDocument, CancellationToken.None);
    }

    [TestMethod]
    public async Task TestSerializeStream()
    {
      var laptopXmlDocument = new LaptopXmlDocument
      {
        Sku = "test",
        Title = "Test Test",
        Description = "Test test test.",
        ScreenSize = "test",
        Processor = "test",
        RamVolume = "test",
      };

      using (var stream = new MemoryStream())
      {
        await _serializer.SerializeAsync(laptopXmlDocument, stream, CancellationToken.None);
      }
    }

    [TestMethod]
    public void TestDeserialize()
    {
      var laptopXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""test"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</product>";
      var laptopXmlDocument = _serializer.DeserializeAsync(
        laptopXml, typeof(LaptopXmlDocument), CancellationToken.None);
    }

    [TestMethod]
    public async Task TestDeserializeStream()
    {
      var laptopXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""test"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</product>";

      using (var stream = new MemoryStream())
      using (var writer = new StreamWriter(stream))
      {
        await writer.WriteLineAsync(laptopXml);
        await writer.FlushAsync();

        var laptopXmlDocument = _serializer.DeserializeAsync(
          stream, typeof(LaptopXmlDocument), CancellationToken.None);
      }
    }

    [TestMethod]
    public void TestDeserializeGeneric()
    {
      var laptopXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""test"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</product>";
      var laptopXmlDocument = _serializer.DeserializeAsync<LaptopXmlDocument>(
        laptopXml, CancellationToken.None);
    }

    [TestMethod]
    public async Task TestDeserializeGenericStream()
    {
      var laptopXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""test"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</product>";
      using (var stream = new MemoryStream())
      using (var writer = new StreamWriter(stream))
      {
        await writer.WriteLineAsync(laptopXml);
        await writer.FlushAsync();

        var laptopXmlDocument = _serializer.DeserializeAsync<LaptopXmlDocument>(
          stream, CancellationToken.None);
      }
    }
  }
}
