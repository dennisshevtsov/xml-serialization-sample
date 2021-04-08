// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System.Xml.Serialization;

  public sealed class OpticalMouseXmlDocument : ProductXmlDocumentBase
  {
    [XmlElement("dpi")]
    public int OpticalTrackingDpi { get; set; }

    [XmlElement("buttons")]
    public int Buttons { get; set; }
  }
}
