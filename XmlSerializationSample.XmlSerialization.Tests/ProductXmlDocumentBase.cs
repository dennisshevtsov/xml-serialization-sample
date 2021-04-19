// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  public abstract class ProductXmlDocumentBase
  {
    public string Sku { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
  }
}
