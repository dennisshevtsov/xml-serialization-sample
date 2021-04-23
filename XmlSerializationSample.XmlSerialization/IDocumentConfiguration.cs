// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System.Xml.Serialization;

  /// <summary>Provides a simple API to configure an XML serialization of a type.</summary>
  /// <typeparam name="TDocument">A type of a document.</typeparam>
  public interface IDocumentConfiguration<TDocument>
  {
    /// <summary>Configures XML overrides.</summary>
    /// <param name="overrides">An object that allows you to override property, field, and class attributes when you use the <see cref="System.Xml.Serialization.XmlSerializer"/> to serialize or deserialize an object.</param>
    public void Configure(XmlAttributeOverrides overrides);
  }
}
