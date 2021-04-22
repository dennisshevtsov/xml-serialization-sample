// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  using System.Xml.Linq;

  using Microsoft.VisualStudio.TestTools.UnitTesting;

  public static class TestChecker
  {
    public static void Check(LaptopDocument document, string xml)
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

    public static void Check(LaptopDocument expected, LaptopDocument actual)
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
