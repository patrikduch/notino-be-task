//-----------------------------------------------------------------------------------
// <copyright file="ExchangeXmlToJsonCommandRequest.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeXmlToJson;

using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Mediator command request for exchaning XML file to JSON.
/// </summary>
public class ExchangeXmlToJsonCommandRequest : IRequest<Result<byte[]>>
{
    public IFormFile? File { get; set; }
}
