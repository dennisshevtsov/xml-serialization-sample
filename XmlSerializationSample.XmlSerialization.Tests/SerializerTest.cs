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

      services.AddSerialization(config => config.Add(new LaptopDocumentConfiguration()));

      var provider = services.BuildServiceProvider();

      _disposable = provider;
      _serializer = provider.GetRequiredService<ISerializer>();
    }

    [TestCleanup]
    public void Cleanup() => _disposable?.Dispose();

    [TestMethod]
    public async Task TestSerialize()
    {
      var document = TestGenerator.GenerateLaptop();
      var xml = await _serializer.SerializeAsync(document, CancellationToken.None);

      TestChecker.Check(document, xml);
    }

    [TestMethod]
    public async Task TestSerializeStream()
    {
      using (var stream = new MemoryStream())
      {
        var document = TestGenerator.GenerateLaptop();

        await _serializer.SerializeAsync(document, stream, CancellationToken.None);

        using (var reader = new StreamReader(stream, Encoding.UTF8))
        {
          stream.Seek(0, SeekOrigin.Begin);

          var xml = await reader.ReadToEndAsync();

          TestChecker.Check(document, xml);
        }
      }
    }

    [TestMethod]
    public async Task TestDeserialize()
    {
      var expected = TestGenerator.GenerateLaptop();
      var xml = TestGenerator.GenerateXml(expected);
      var document = await _serializer.DeserializeAsync(
        xml, typeof(LaptopDocument), CancellationToken.None);

      Assert.IsNotNull(document);

      var actual = document as LaptopDocument;

      TestChecker.Check(expected, actual);
    }

    [TestMethod]
    public async Task TestDeserializeStream()
    {
      var expected = TestGenerator.GenerateLaptop();
      var xml = TestGenerator.GenerateXml(expected);

      using (var stream = new MemoryStream())
      using (var writer = new StreamWriter(stream))
      {
        await writer.WriteLineAsync(xml);
        await writer.FlushAsync();

        var document = await _serializer.DeserializeAsync(
          stream, typeof(LaptopDocument), CancellationToken.None);

        Assert.IsNotNull(document);

        var actual = document as LaptopDocument;

        TestChecker.Check(expected, actual);
      }
    }

    [TestMethod]
    public async Task TestDeserializeGeneric()
    {
      var expected = TestGenerator.GenerateLaptop();
      var xml = TestGenerator.GenerateXml(expected);
      var actual = await _serializer.DeserializeAsync<LaptopDocument>(
        xml, CancellationToken.None);

      TestChecker.Check(expected, actual);
    }

    [TestMethod]
    public async Task TestDeserializeGenericStream()
    {
      var expected = TestGenerator.GenerateLaptop();
      var xml = TestGenerator.GenerateXml(expected);

      using (var stream = new MemoryStream())
      using (var writer = new StreamWriter(stream))
      {
        await writer.WriteLineAsync(xml);
        await writer.FlushAsync();

        var actual = await _serializer.DeserializeAsync<LaptopDocument>(
          stream, CancellationToken.None);

        TestChecker.Check(expected, actual);
      }
    }
  }
}
