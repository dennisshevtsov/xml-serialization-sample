// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace XmlSerializationSample.XmlSerialization
{
  using System;

  using Microsoft.Extensions.DependencyInjection;

  public static class ServicesExtensions
  {
    public static IServiceCollection AddSerialization(
      this IServiceCollection services, Action<ISerializerConfig> configure)
    {
      if (services == null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      if (configure == null)
      {
        throw new ArgumentNullException(nameof(configure));
      }

      var provider = new SerializerProvider();

      configure.Invoke(provider);

      services.AddSingleton(provider);

      return services;
    }
  }
}
