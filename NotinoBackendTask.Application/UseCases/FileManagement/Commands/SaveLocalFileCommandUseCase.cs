//---------------------------------------------------------------------------------
// <copyright file="SaveLocalFileCommandUseCase.json" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.UseCases.FileManagement.Commands;

using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Application.Enums;
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.SaveLocalFile;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Bussiness logic for saving new content into specifed  absolute filepath.
/// </summary>
public class SaveLocalFileCommandUseCase : IRequestHandler<SaveLocalFileCommandRequest, Result<string>>
{
    private readonly IFileUtils _fileUtils;
    private readonly IJsonFileUtils _jsonFileUtils;
    private readonly ILogger<SaveLocalFileCommandUseCase> _logger;
    private readonly IXmlFileUtils _xmlFileUtils;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="SaveLocalFileCommandUseCase"/> class.
    /// </summary>
    /// <param name="logger"><seealso cref="ILogger"/> logger object of <seealso cref="SaveLocalFileCommandUseCase"/> class.</param>
    /// <param name="fileUtils"></param>
    
    public SaveLocalFileCommandUseCase(IFileUtils fileUtils, IJsonFileUtils jsonFileUtils, ILogger<SaveLocalFileCommandUseCase> logger, IXmlFileUtils xmlFileUtils)
    {
        _fileUtils = fileUtils;
        _jsonFileUtils = jsonFileUtils;
        _logger = logger;
        _xmlFileUtils = xmlFileUtils;
    }

    /// <summary>
    /// Interceptor for handling file persistence inside local storage.
    /// </summary>
    /// <param name="request">Intercepting<seealso cref="SaveLocalFileQueryCommand"/> object.</param>
    /// <param name="cancellationToken"><seealso cref="CancellationToken"/> dependency object.</param>
    /// <returns>String representation of changed file content.</returns>
    public async Task<Result<string>> Handle(SaveLocalFileCommandRequest request, CancellationToken cancellationToken)
    {
        ValidationException validationException;

        var exchangeJsonToXmlCommandValidator = new SaveLocalFileCommandValidator();
        var validationResult = await exchangeJsonToXmlCommandValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            validationException = new ValidationException(validationResult.Errors);
            return new Result<string>(validationException);
        }

        // Check file extension
        var extension = _fileUtils.GetExtension(request.AbsoluteDestinationFilePath);

        if (extension.Equals(FileExtensions.JSON))
        {
            var jsonContent = _jsonFileUtils.WriteLocal(request.AbsoluteDestinationFilePath, request.Document);

            return await Task.FromResult(jsonContent);
        }

        if (extension.Equals(FileExtensions.XML))
        {
            var xmlContent = _xmlFileUtils.WriteLocal(request.AbsoluteDestinationFilePath, request.Document);

            return await Task.FromResult(xmlContent);
        }

        var fileIsNullFailure = new ValidationResult
        {
            Errors = new List<ValidationFailure>
                {
                    new ValidationFailure
                    {
                        ErrorCode = "400",
                        ErrorMessage = "Invalid file, check provided file or its extension."
                    }
                }
        };

        _logger.LogError("Invalid file, check provided file or its extension.");

        validationException = new ValidationException(fileIsNullFailure.Errors);
        return new Result<string>(validationException);
    }
}
