// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using System;

  using Microsoft.IO;

  using XmlSerializationSample.XmlSerialization;

  public static class ServicesExtensions
  {
    public static IServiceCollection AddSerialization(
      this IServiceCollection services, Action<ISerializerConfiguration> configure)
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

      services.AddSingleton<ISerializerProvider>(provider);
      services.AddSingleton<RecyclableMemoryStreamManager>();
      services.AddScoped<ISerializer, Serializer>();

      return services;
    }
  }
}
