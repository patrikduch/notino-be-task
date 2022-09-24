//-------------------------------------------------------------------------------
// <copyright file="FileUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Infrastructure.Helpers.FileManagement;

using Microsoft.AspNetCore.Http;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;

/// <summary>
/// Helper for management of filetorage.
/// </summary>
public class FileUtils : IFileUtils
{
    /// <summary>
    /// Get extension of particular filename.
    /// </summary>
    /// <param name="filePath">Path to particular file.</param>
    /// <returns>File extension.</returns>
    public string GetExtension(string filePath)
    {
        return Path.GetExtension(filePath);
    }

    /// <summary>
    /// Load file from in-memory <seealso cref="IFormFile"/> object.
    /// </summary>
    /// <param name="file">Input <seealso cref="IFormFile"/>.</param>
    /// <returns>Content of loaded file.</returns>
    public string LoadFile(IFormFile file)
    {
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            return reader.ReadToEnd();
        }
    }
}
