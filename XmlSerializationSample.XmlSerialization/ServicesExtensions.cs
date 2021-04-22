// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using System;

  using Microsoft.IO;

  using XmlSerializationSample.XmlSerialization;

  /// <summary>Provides methods to extend an API of the <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> class.</summary>
  public static class ServicesExtensions
  {
    /// <summary>Registers serialization services.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <param name="configure">An object that provides a simple API to register an XML configuration of a type.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
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
