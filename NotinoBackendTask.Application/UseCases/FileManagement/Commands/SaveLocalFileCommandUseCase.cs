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
using Newtonsoft.Json;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Application.Enums;
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.SaveLocalFile;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Bussiness logic for saving new content into specifed  absolute filepath.
/// </summary>
public class SaveLocalFileCommandUseCase : IRequestHandler<SaveLocalFileQueryCommand, Result<string>>
{
    private readonly ILogger<SaveLocalFileCommandUseCase> _logger;
    private readonly IXmlFileUtils _xmlFileUtils;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="SaveLocalFileCommandUseCase"/> class.
    /// </summary>
    /// <param name="logger"><seealso cref="ILogger"/> logger object of <seealso cref="SaveLocalFileCommandUseCase"/> class.</param>
    /// <param name="xmlFileUtils"></param>
    public SaveLocalFileCommandUseCase(ILogger<SaveLocalFileCommandUseCase> logger, IXmlFileUtils xmlFileUtils)
    {
        _logger = logger;
        _xmlFileUtils = xmlFileUtils;
    }

    /// <summary>
    /// Interceptor for handling file persistence inside local storage.
    /// </summary>
    /// <param name="request">Intercepting<seealso cref="SaveLocalFileQueryCommand"/> object.</param>
    /// <param name="cancellationToken"><seealso cref="CancellationToken"/> dependency object.</param>
    /// <returns>String representation of changed file content.</returns>
    public async Task<Result<string>> Handle(SaveLocalFileQueryCommand request, CancellationToken cancellationToken)
    {
        // Check file extension
        var extension = Path.GetExtension(request.AbsoluteDestinationFilePath);

        if (extension.Equals(FileExtensions.JSON))
        {
            string json = JsonConvert.SerializeObject(request.Document);
            File.WriteAllText(request.AbsoluteDestinationFilePath, json);

            return await Task.FromResult(json);
        }

        if (extension.Equals(FileExtensions.XML))
        {
            var xmlString = _xmlFileUtils.ConvertAnyObjectToXml(request.Document);
            File.WriteAllText(request.AbsoluteDestinationFilePath, xmlString);

            return await Task.FromResult(xmlString);
        }

        var fileIsNullFailure = new ValidationResult
        {
            Errors = new List<ValidationFailure>
                {
                    new ValidationFailure
                    {
                        ErrorCode = "400",
                        ErrorMessage = "File wasn't changed."
                    }
                }
        };

        _logger.LogError("File wasn't changed.");

        var validationException = new ValidationException(fileIsNullFailure.Errors);
        return new Result<string>(validationException);
    }
}
