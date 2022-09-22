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

/// <summary>
/// Utilities for management XML files.
/// </summary>
public class XmlFileUtils : IXmlFileUtils
{
    /// <summary>
    /// Convert XML to Json exchange file format.
    /// </summary>
    /// <param name="fileContent">File input string.</param>
    /// <returns></returns>
    public string ConvertXmltoJson(string fileContent)
    {
        var xdoc = XDocument.Parse(fileContent);
        return JsonConvert.SerializeObject(xdoc.Root);
    }
}
