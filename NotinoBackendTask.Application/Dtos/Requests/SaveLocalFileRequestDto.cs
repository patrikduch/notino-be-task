//-------------------------------------------------------------------------------
// <copyright file="SaveLocalFileRequestDto.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
// -------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Dtos.Requests;

using Microsoft.AspNetCore.Http;

/// <summary>
/// Data transfer object for saving new content into particular file.
/// </summary>
/// <param name="Document">Object which consists new data that will be processed.</param>

public record SaveLocalFileRequestDto(string AbsoluteDestinationFilePath, DocumentDto Document);