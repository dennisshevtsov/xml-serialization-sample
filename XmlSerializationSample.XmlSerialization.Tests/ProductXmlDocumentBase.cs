﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
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