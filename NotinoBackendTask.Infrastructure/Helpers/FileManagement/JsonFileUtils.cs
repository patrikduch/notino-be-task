//-------------------------------------------------------------------------------
// <copyright file="JsonFileUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Infrastructure.Helpers.FileManagement;

using Newtonsoft.Json;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;


/// <summary>
/// Utilities for management JSON files.
/// </summary>
public class JsonFileUtils : IJsonFileUtils 
{
    private static readonly JsonSerializerSettings _options
        = new() { NullValueHandling = NullValueHandling.Ignore };

    /// <summary>
    /// Conversion file content to byte array.
    /// </summary>
    /// <param name="fileContent">Content of particular JSON file.</param>
    /// <returns></returns>
    public byte[] SerializeToUtf8Bytes(object fileContent)
    {
        using var stream = new MemoryStream();
        using var streamWriter = new StreamWriter(stream);
        using var jsonWriter = new JsonTextWriter(streamWriter);
        JsonSerializer.CreateDefault(_options).Serialize(jsonWriter, fileContent);
        jsonWriter.Flush();
        stream.Position = 0;
        return stream.ToArray();
    }
}
