﻿//-----------------------------------------------------------------------------------
// <copyright file="LoadLocalFileQueryRequest.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.FileManagement.Queries;

using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Mediator query request for loading file from selected local path..
/// </summary>
public class LoadLocalFileQueryRequest : IRequest<Result<string>>
{
    public IFormFile? File { get; set; } = default;
}
