namespace XmlSerializationSample.XmlSerialization
{
  using System.Xml.Serialization;

  [XmlRoot("product")]
  public abstract class ProductXmlDocumentBase
  {
    [XmlAttribute("sku")]
    public string Sku { get; set; }

    [XmlElement("title")]
    public string Title { get; set; }

    [XmlElement("description")]
    public string Description { get; set; }
  }
}
