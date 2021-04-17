// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  using System;
  using System.IO;
  using System.Threading;
  using System.Threading.Tasks;

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
        config => config.Add(new LaptopDocumentConfiguration())
                        .Add(new OpticalMouseDocumentConfiguration()));

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
    }

    [TestMethod]
    public async Task TestSerializeStream()
    {
      using (var stream = new MemoryStream())
      {
        var document = SerializerTest.GenerateLaptop();

        await _serializer.SerializeAsync(document, stream, CancellationToken.None);
      }
    }

    [TestMethod]
    public void TestDeserialize()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""test"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</product>";
      var document = _serializer.DeserializeAsync(
        xml, typeof(LaptopDocument), CancellationToken.None);
    }

    [TestMethod]
    public async Task TestDeserializeStream()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
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
        await writer.WriteLineAsync(xml);
        await writer.FlushAsync();

        var document = _serializer.DeserializeAsync(
          stream, typeof(LaptopDocument), CancellationToken.None);
      }
    }

    [TestMethod]
    public void TestDeserializeGeneric()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""test"">
  <title>Test Test</title>
  <description>Test test test.</description>
  <screen>test</screen>
  <processor>test</processor>
  <ram>test</ram>
</product>";
      var document = _serializer.DeserializeAsync<LaptopDocument>(
        xml, CancellationToken.None);
    }

    [TestMethod]
    public async Task TestDeserializeGenericStream()
    {
      var xml = @"<?xml version=""1.0"" encoding=""utf-8""?>
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
        await writer.WriteLineAsync(xml);
        await writer.FlushAsync();

        var document = _serializer.DeserializeAsync<LaptopDocument>(
          stream, CancellationToken.None);
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
  }
}
