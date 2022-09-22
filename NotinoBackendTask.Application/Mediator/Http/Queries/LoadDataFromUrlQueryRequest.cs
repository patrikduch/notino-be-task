//-----------------------------------------------------------------------------------
// <copyright file="LoadDataFromUrlQueryRequest.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.Http.Queries;

using LanguageExt.Common;
using MediatR;

/// <summary>
/// Mediator query request for loading data from HTTP URL.
/// </summary>
public class LoadDataFromUrlQueryRequest : IRequest<Result<string>>
{
    public string Url { get; set; } = string.Empty;
}
