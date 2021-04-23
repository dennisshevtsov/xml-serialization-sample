// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.Xml.Serialization;

  /// <summary>Provides a simple API to get an instance of the <see cref="System.Xml.Serialization.XmlSerializer"/> class for a type of a serializing object.</summary>
  public interface ISerializerProvider
  {
    /// <summary>Gets an instance of the <see cref="System.Xml.Serialization.XmlSerializer"/> class for a type of a serializing object.</summary>
    /// <param name="type">An object that represents a type of an object that is required to serialize.</param>
    /// <returns>An object that serializes and deserializes objects into and from XML documents. The <see cref="System.Xml.Serialization.XmlSerializer"/> enables you to control how objects are encoded into XML.</returns>
    public XmlSerializer Get(Type type);
  }
}
