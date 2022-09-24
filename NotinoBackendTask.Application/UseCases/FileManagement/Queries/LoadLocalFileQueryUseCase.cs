//---------------------------------------------------------------------------------
// <copyright file="LoadLocalFileQueryUseCase.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------------


namespace NotinoBackendTask.Application.UseCases.FileManagement.Queries;

using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Application.Mediator.FileManagement.Queries;
using System.Threading;
using System.Threading.Tasks;


/// <summary>
/// Bussiness logic for reading content of locally placed files.
/// </summary>
public class LoadLocalFileQueryUseCase : IRequestHandler<LoadLocalFileQueryRequest, Result<string>>
{
    private readonly IFileUtils _fileUtils;
    private readonly ILogger<LoadLocalFileQueryUseCase> _logger;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="LoadLocalFileQueryUseCase"/> class.
    /// </summary>
    public LoadLocalFileQueryUseCase(IFileUtils fileUtils, ILogger<LoadLocalFileQueryUseCase> logger)
    {
        _fileUtils = fileUtils;
        _logger = logger;
    }

    public async Task<Result<string>> Handle(LoadLocalFileQueryRequest request, CancellationToken cancellationToken)
    {
        if (request.File is null)
        {
            var validationResult = new ValidationResult
            {
                Errors = new List<ValidationFailure>
                {
                    new ValidationFailure
                    {
                        ErrorCode = "400",
                        ErrorMessage = "File cannot be null"
                    }
                }
            };

            _logger.LogError("File cannot be null");

            var validationException = new ValidationException(validationResult.Errors);
            return new Result<string>(validationException);
        }

        return await Task.FromResult(_fileUtils.LoadFile(request.File));
    }
}
