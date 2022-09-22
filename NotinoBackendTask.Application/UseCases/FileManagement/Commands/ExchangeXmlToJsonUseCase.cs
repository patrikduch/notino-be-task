//---------------------------------------------------------------------------------
// <copyright file="ExchangeXmlToJsonUseCase.json" website="Patrikduch.com">
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
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeXmlToJson;

/// <summary>
/// Bussiness logic for exchanging XML to Json file format.
/// </summary>
public class ExchangeXmlToJsonUseCase : IRequestHandler<ExchangeXmlToJsonCommandRequest, Result<byte[]>>
{
    private readonly IFileUtils _fileUtils;
    private readonly ILogger<ExchangeXmlToJsonUseCase> _logger;
    private readonly IJsonFileUtils _jsonFileUtils;
    private readonly IXmlFileUtils _xmlFileUtils;

    public ExchangeXmlToJsonUseCase(IFileUtils fileUtils, ILogger<ExchangeXmlToJsonUseCase> logger, IJsonFileUtils jsonFileUtils, IXmlFileUtils xmlFileUtils)
    {
        _fileUtils = fileUtils;
        _logger = logger;
        _jsonFileUtils = jsonFileUtils;
        _xmlFileUtils = xmlFileUtils;
    }

    public async Task<Result<byte[]>> Handle(ExchangeXmlToJsonCommandRequest request, CancellationToken cancellationToken)
    {
        var exchangeXmlToJsonCommandValidator = new ExchangeXmlToJsonCommandValidator();
        var validationResult = await exchangeXmlToJsonCommandValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<byte[]>(validationException);
        }

        if (request.File is null)
        {
            var fileIsNullFailure = new ValidationResult
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

            var validationException = new ValidationException(fileIsNullFailure.Errors);
            return new Result<byte[]>(validationException);
        }


        var fileContent = _fileUtils.LoadFile(request.File);    
        var jsonContent = _xmlFileUtils.ConvertXmltoJson(fileContent);

            
        return await Task.FromResult(_jsonFileUtils.SerializeToUtf8Bytes(jsonContent));
    }
}
