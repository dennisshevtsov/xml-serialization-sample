// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  using System;
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
      var laptopXml = await _serializer.SerializeAsync(laptopXmlDocument);
    }

    [TestMethod]
    public void TestDeserialize()
    {
      var laptopXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<product d1p1:type=""LaptopXmlDocument"" sku=""test"" xmlns:d1p1=""http://www.w3.org/2001/XMLSchema-instance"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</product>";
      var laptopXmlDocument = _serializer.Deserialize<LaptopXmlDocument>(laptopXml);
    }

    [TestMethod]
    public void TestDeserialize1()
    {
      var laptopXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""test"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</product>";
      var laptopXmlDocument = _serializer.Deserialize<LaptopXmlDocument>(laptopXml);
    }

    [TestMethod]
    public void TestDeserialize2()
    {
      var laptopXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<LaptopXmlDocument sku=""test"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</LaptopXmlDocument>";
      var laptopXmlDocument = _serializer.Deserialize<LaptopXmlDocument>(laptopXml);
    }
  }
}
