//-----------------------------------------------------------------------
// <copyright file="IJsonFileUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------
using NotinoBackendTask.Application.Dtos;

namespace NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;

/// <summary>
/// Contract for JsonFile Utils helper.
/// </summary>
public interface IJsonFileUtils
{
    byte[] SerializeToUtf8Bytes(object fileContent);

    string WriteLocal(string filename, DocumentDto? document);
}