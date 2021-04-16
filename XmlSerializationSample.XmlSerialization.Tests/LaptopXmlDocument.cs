// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  using System.Xml.Serialization;

  public sealed class LaptopXmlDocument : ProductXmlDocumentBase
  {
    [XmlElement("screen")]
    public string ScreenSize { get; set; }

    [XmlElement("processor")]
    public string Processor { get; set; }

    [XmlElement("ram")]
    public string RamVolume { get; set; }
  }
}
