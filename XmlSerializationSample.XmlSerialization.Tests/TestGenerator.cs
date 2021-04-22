// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization.Tests
{
  using System;

  public static class TestGenerator
  {
    public const int DefaultRandomTokenSize = 5;

    public static string GenerateToken(int length = TestGenerator.DefaultRandomTokenSize) =>
          Guid.NewGuid()
              .ToString()
              .Replace("-", "")
              .Substring(0, length)
              .ToUpper();

    public static LaptopDocument GenerateLaptop() =>
      new LaptopDocument
      {
        Sku = TestGenerator.GenerateToken(),
        Title = TestGenerator.GenerateToken(),
        Description = TestGenerator.GenerateToken(),
        ScreenSize = TestGenerator.GenerateToken(),
        Processor = TestGenerator.GenerateToken(),
        RamVolume = TestGenerator.GenerateToken(),
      };

    public static string GenerateXml(LaptopDocument document) =>
@$"<?xml version=""1.0"" encoding=""utf-8""?>
<product sku=""{document.Sku}"">
  <title>{document.Title}</title>
  <description>{document.Description}</description>
  <screen-size>{document.ScreenSize}</screen-size>
  <processor>{document.Processor}</processor>
  <ram-volume>{document.RamVolume}</ram-volume>
</product>";
  }
}
