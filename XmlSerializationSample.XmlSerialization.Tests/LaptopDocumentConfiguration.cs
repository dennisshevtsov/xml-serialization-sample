// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  using System.Xml.Serialization;

  public sealed class LaptopDocumentConfiguration : IDocumentConfiguration<LaptopDocument>
  {
    public void Configure(XmlAttributeOverrides overrides)
    {
      var attributes1 = new XmlAttributes();
      attributes1.XmlRoot = new XmlRootAttribute("product");

      overrides.Add(typeof(LaptopDocument), attributes1);

      var attributes0 = new XmlAttributes();
      attributes0.XmlAttribute = new XmlAttributeAttribute("sku");

      overrides.Add(typeof(ProductXmlDocumentBase), nameof(ProductXmlDocumentBase.Sku), attributes0);

      var attributes2 = new XmlAttributes();
      attributes2.XmlElements.Add(new XmlElementAttribute("title"));

      overrides.Add(typeof(ProductXmlDocumentBase), nameof(ProductXmlDocumentBase.Title), attributes2);

      var attributes3 = new XmlAttributes();
      attributes3.XmlElements.Add(new XmlElementAttribute("description"));

      overrides.Add(typeof(ProductXmlDocumentBase), nameof(ProductXmlDocumentBase.Description), attributes3);

      var attributes4 = new XmlAttributes();
      attributes4.XmlElements.Add(new XmlElementAttribute("screen-size"));

      overrides.Add(typeof(LaptopDocument), nameof(LaptopDocument.ScreenSize), attributes4);

      var attributes5 = new XmlAttributes();
      attributes5.XmlElements.Add(new XmlElementAttribute("processor"));

      overrides.Add(typeof(LaptopDocument), nameof(LaptopDocument.Processor), attributes5);

      var attributes6 = new XmlAttributes();
      attributes6.XmlElements.Add(new XmlElementAttribute("ram-volume"));

      overrides.Add(typeof(LaptopDocument), nameof(LaptopDocument.RamVolume), attributes6);
    }
  }
}
