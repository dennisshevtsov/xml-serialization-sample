using System.Xml.Serialization;

namespace XmlSerializationSample.XmlSerialization
{
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
