﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;
  using System.Xml.Serialization;

  public sealed class SerializerProvider : ISerializerProvider, ISerializerConfig
  {
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
