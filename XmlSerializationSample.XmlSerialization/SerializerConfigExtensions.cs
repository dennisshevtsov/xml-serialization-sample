// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.Xml.Serialization;

  public static class SerializerConfigExtensions
  {
    public static ISerializerConfig Add<TDocument>(
      this ISerializerConfig config,
      IDocumentConfiguration<TDocument> configuration)
      where TDocument : class
    {
      if (config == null)
      {
        throw new ArgumentNullException(nameof(config));
      }

      var overrides = new XmlAttributeOverrides();

      configuration.Configure(overrides);

      config.Add(typeof(TDocument), overrides);

      return config;
    }
  }
}
