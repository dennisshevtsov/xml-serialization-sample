// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.Collections.Generic;
  using System.Xml.Serialization;

  public sealed class SerializerProvider : ISerializerProvider, ISerializerConfig
  {
    private readonly IDictionary<Type, XmlAttributeOverrides> _overridesDictionary;

    public SerializerProvider()
      => _overridesDictionary = new Dictionary<Type, XmlAttributeOverrides>();

    public XmlSerializer Get(Type type)
    {
      throw new NotImplementedException();
    }

    public ISerializerConfig Add(Type type, XmlAttributeOverrides overrides)
    {
      throw new NotImplementedException();
    }
  }
}
