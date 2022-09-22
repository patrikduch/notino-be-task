//------------------------------------------------------------------------------------
// <copyright file="InfrastructureServicesRegistration.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//------------------------------------------------------------------------------------

namespace NotinoBackendTask.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Infrastructure.Helpers.Email;
using NotinoBackendTask.Infrastructure.Helpers.FileManagement;

/// <summary>
/// Registration of Infrastructure layer services.
/// </summary>
public static class InfrastructureServicesRegistration
{
    /// <summary>
    /// Registration Infrastructure services into Dependency Injection container.
    /// </summary>
    /// <param name="services"><seealso cref="IServiceCollection"/> object.</param>
    /// <param name="configuration"><seealso cref="IConfiguration"/> object.</param>
    /// <returns><seealso cref="IServiceCollection"/> object.</returns>
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFileUtils, FileUtils>();
        services.AddScoped<IJsonFileUtils, JsonFileUtils>();
        services.AddScoped<IEmailUtils, EmailUtils>();
        services.AddScoped<IXmlFileUtils, XmlFileUtils>();

        return services;
    }
}