// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System.Xml.Serialization;

  public interface IDocumentConfiguration<TDocument>
  {
    public void Configure(XmlAttributeOverrides overrides);
  }
}
