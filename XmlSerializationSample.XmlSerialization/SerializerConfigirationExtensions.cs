// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.Xml.Serialization;

  /// <summary>Provides methods to extend an API of the <see cref="XmlSerializationSample.XmlSerialization.ISerializerConfiguration"/> class.</summary>
  public static class SerializerConfigirationExtensions
  {
    /// <summary>Registers XML overrides of a type.</summary>
    /// <typeparam name="TDocument">A type of an object that is required to serialize/deserialize.</typeparam>
    /// <param name="serializerConfiguration">An object that provides a simple API to register an XML configuration of a type.</param>
    /// <param name="documentConfiguration">An object that provides a simple API to configure an XML serialization of a type.</param>
    /// <returns>An object that provides a simple API to register an XML configuration of a type.</returns>
    public static ISerializerConfiguration Add<TDocument>(
      this ISerializerConfiguration serializerConfiguration,
      IDocumentConfiguration<TDocument> documentConfiguration)
      where TDocument : class
    {
      if (serializerConfiguration == null)
      {
        throw new ArgumentNullException(nameof(serializerConfiguration));
      }

      if (documentConfiguration == null)
      {
        throw new ArgumentNullException(nameof(documentConfiguration));
      }

      var overrides = new XmlAttributeOverrides();

      documentConfiguration.Configure(overrides);

      serializerConfiguration.Add(typeof(TDocument), overrides);

      return serializerConfiguration;
    }
  }
}
