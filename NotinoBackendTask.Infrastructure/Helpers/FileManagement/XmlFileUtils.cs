//-------------------------------------------------------------------------------
// <copyright file="XmlFileUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Infrastructure.Helpers.FileManagement;

using Newtonsoft.Json;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using System.Xml.Linq;
using System.Xml.Serialization;

/// <summary>
/// Utilities for management XML files.
/// </summary>
public class XmlFileUtils : IXmlFileUtils
{
    /// <summary>
    /// Convert any object to string with XML structure.
    /// </summary>
    /// <param name="obj">Any object of C# programming language.</param>
    /// <returns>String content with XML structure.</returns>
    /// <exception cref="NotImplementedException"></exception>
    public string ConvertAnyObjectToXml(object obj)
    {
        using (var writer = new StringWriter())
        {
            new XmlSerializer(obj.GetType()).Serialize(writer, obj);
            return writer.ToString();
        }
    }

    /// <summary>
    /// Convert XML to Json exchange file format.
    /// </summary>
    /// <param name="fileContent">File input string.</param>
    /// <returns>String content with JSON structure.</returns>
    public string ConvertXmltoJson(string fileContent)
    {
        var xdoc = XDocument.Parse(fileContent);
        return JsonConvert.SerializeObject(xdoc.Root);
    }
}
