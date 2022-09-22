//-----------------------------------------------------------------------
// <copyright file="IFileUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;

using Microsoft.AspNetCore.Http;

/// <summary>
/// Contract for FileUtils helper.
/// </summary>
public interface IFileUtils
{
    string LoadFile(IFormFile file);
}
