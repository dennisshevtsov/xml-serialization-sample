// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  /// <summary>Provides a simple API to get an instance of the <see cref="System.Xml.Serialization.XmlSerializer"/> class for a type of a serializing object.</summary>
  public sealed class SerializerProvider : ISerializerProvider, ISerializerConfiguration
  {
    private readonly IDictionary<Type, XmlAttributeOverrides> _overridesDictionary;

    /// <summary>Initializes a new instance of the <see cref="XmlSerializationSample.XmlSerialization.SerializerProvider"/> class.</summary>
    public SerializerProvider()
      => _overridesDictionary = new Dictionary<Type, XmlAttributeOverrides>();

    /// <summary>Gets an instance of the <see cref="System.Xml.Serialization.XmlSerializer"/> class for a type of a serializing object.</summary>
    /// <param name="type">An object that represents a type of an object that is required to serialize.</param>
    /// <returns>An object that serializes and deserializes objects into and from XML documents. The <see cref="System.Xml.Serialization.XmlSerializer"/> enables you to control how objects are encoded into XML.</returns>
    public XmlSerializer Get(Type type) => new XmlSerializer(type, _overridesDictionary[type]);

    /// <summary>Registers XML overrides of a type.</summary>
    /// <param name="type">An object that represent a type declaration of a serializing object.</param>
    /// <param name="overrides">An object that allows you to override property, field, and class attributes when you use the <see cref="System.Xml.Serialization.XmlSerializer"/> to serialize or deserialize an object.</param>
    /// <returns>An object that provides a simple API to register an XML configuration of a type.</returns>
    public ISerializerConfiguration Add(Type type, XmlAttributeOverrides overrides)
    {
      _overridesDictionary.Add(type, overrides);

      return this;
    }
  }
}
