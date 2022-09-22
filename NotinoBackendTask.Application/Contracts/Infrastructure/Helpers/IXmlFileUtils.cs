//-----------------------------------------------------------------------
// <copyright file="IXmlFileUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;

/// <summary>
/// Contract for XML utility helper.
/// </summary>
public interface IXmlFileUtils
{
    string ConvertXmltoJson(string fileContent);
}
