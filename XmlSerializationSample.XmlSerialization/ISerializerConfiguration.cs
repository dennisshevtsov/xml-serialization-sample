// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.Xml.Serialization;

  /// <summary>Provides a simple API to register an XML configuration of a type.</summary>
  public interface ISerializerConfiguration
  {
    /// <summary>Registers XML overrides of a type.</summary>
    /// <param name="type">An object that represent a type declaration of a serializing object.</param>
    /// <param name="overrides">An object that allows you to override property, field, and class attributes when you use the <see cref="System.Xml.Serialization.XmlSerializer"/> to serialize or deserialize an object.</param>
    /// <returns>An object that provides a simple API to register an XML configuration of a type.</returns>
    public ISerializerConfiguration Add(Type type, XmlAttributeOverrides overrides);
  }
}
