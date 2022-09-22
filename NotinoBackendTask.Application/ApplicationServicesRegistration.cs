//-------------------------------------------------------------------------------
// <copyright file="ApplicationServicesRegistration.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Application;

using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

/// <summary>
/// Registration of Application layer services.
/// </summary>
public static class ApplicationServicesRegistration
{
    /// <summary>
    /// Registration Application services into Dependency Injection container.
    /// </summary>
    /// <param name="services"><seealso cref="IServiceCollection"/> object.</param>
    /// <returns><seealso cref="IServiceCollection"/> object.</returns>
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}
