//-----------------------------------------------------------------------
// <copyright file="IXmlFileUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

using NotinoBackendTask.Application.Dtos;

namespace NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;

/// <summary>
/// Contract for XML utility helper.
/// </summary>
public interface IXmlFileUtils
{
    string ConvertXmltoJson(string fileContent);

    string ConvertAnyObjectToXml(object obj);

    string WriteLocal(string filename, DocumentDto? document);
}
