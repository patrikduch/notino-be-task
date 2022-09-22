//-------------------------------------------------------------------------------
// <copyright file="ExchangeJsonToXmlRequestDto.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Dtos.Requests;

/// <summary>
/// Data transfer object for exchaning json file format to XML.
/// </summary>
/// <param name="DestinationFileName">Name of newly created XML file.</param>
public record ExchangeJsonToXmlRequestDto(string DestinationFileName);
