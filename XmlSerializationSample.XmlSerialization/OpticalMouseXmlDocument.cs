using System.Xml.Serialization;

namespace XmlSerializationSample.XmlSerialization
{
  public sealed class OpticalMouseXmlDocument : ProductXmlDocumentBase
  {
    [XmlElement("dpi")]
    public int OpticalTrackingDpi { get; set; }

    [XmlElement("buttons")]
    public int Buttons { get; set; }
  }
}
