//-------------------------------------------------------------------------------
// <copyright file="ExchangeXmlToJsonRequestDto.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Dtos.Requests;

/// <summary>
/// Data transfer object for exchaning XML file format to Json.
/// </summary>
/// <param name="DestinationFileName">Name of newly created Json file.</param>
public record ExchangeXmlToJsonRequestDto(string DestinationFileName);
