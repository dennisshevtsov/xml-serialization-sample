// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  public sealed class LaptopDocument : ProductXmlDocumentBase
  {
    public string ScreenSize { get; set; }

    public string Processor { get; set; }

    public string RamVolume { get; set; }
  }
}
