//-----------------------------------------------------------------------
// <copyright file="IJsonFileUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------
namespace NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;

/// <summary>
/// Contract for JsonFile Utils helper.
/// </summary>
public interface IJsonFileUtils
{
    byte[] SerializeToUtf8Bytes(object fileContent);
}