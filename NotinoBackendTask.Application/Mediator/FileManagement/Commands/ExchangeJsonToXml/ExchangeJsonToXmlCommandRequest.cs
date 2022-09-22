//---------------------------------------------------------------------------------
// <copyright file="ExchangeJsonToXmlCommandRequest.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeJsonToXml;

using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Mediator command request for exchaning Json file to XML.
/// </summary>
public class ExchangeJsonToXmlCommandRequest : IRequest<Result<byte[]>>
{
    public IFormFile? File { get; set; } = default;
}
