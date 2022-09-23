//-----------------------------------------------------------------------------------
// <copyright file="SaveLocalFileQueryCommand.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.FileManagement.Commands.SaveLocalFile;

using LanguageExt.Common;
using MediatR;
using NotinoBackendTask.Application.Dtos;


/// <summary>
/// Mediator command request for saving new content to select file path.
/// </summary>
public class SaveLocalFileQueryCommand : IRequest<Result<string>>
{
    public string AbsoluteDestinationFilePath { get; set; } = string.Empty;

    public DocumentDto? Document { get; set; }
}
