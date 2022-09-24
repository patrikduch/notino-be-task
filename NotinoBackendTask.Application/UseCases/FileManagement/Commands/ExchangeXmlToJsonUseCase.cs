//---------------------------------------------------------------------------------
// <copyright file="ExchangeXmlToJsonUseCase.json" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.UseCases.FileManagement.Commands;

using FluentValidation;
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

    /// <summary>
    /// Initializes a new instance of the <seealso cref="ExchangeXmlToJsonUseCase"/> class.
    /// </summary>
    /// <param name="fileUtils"><seealso cref="IFileUtils"/> dependency object for accessing local file storage.</param>
    /// <param name="logger"><seealso cref="ILogger"/> dependency object for logging purposes.</param>
    /// <param name="jsonFileUtils"><seealso cref="IJsonFileUtils"/> Json helper dependency object.</param>
    /// <param name="xmlFileUtils"><seealso cref="IXmlFileUtils"/> XML helper dependency object.</param>
    public ExchangeXmlToJsonUseCase(IFileUtils fileUtils, ILogger<ExchangeXmlToJsonUseCase> logger, IJsonFileUtils jsonFileUtils, IXmlFileUtils xmlFileUtils)
    {
        _fileUtils = fileUtils;
        _logger = logger;
        _jsonFileUtils = jsonFileUtils;
        _xmlFileUtils = xmlFileUtils;
    }

    /// <summary>
    /// Interceptor for handling file conversion from XML to JSON fileformat.
    /// </summary>
    /// <param name="request">Intercepting <seealso cref="ExchangeXmlToJsonCommandRequest"/> object.</param>
    /// <param name="cancellationToken"><seealso cref="CancellationToken"/> object.</param>
    /// <returns></returns>
    public async Task<Result<byte[]>> Handle(ExchangeXmlToJsonCommandRequest request, CancellationToken cancellationToken)
    {
        var exchangeXmlToJsonCommandValidator = new ExchangeXmlToJsonCommandValidator();
        var validationResult = await exchangeXmlToJsonCommandValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<byte[]>(validationException);
        }


        var fileContent = _fileUtils.LoadFile(request.File);    
        var jsonContent = _xmlFileUtils.ConvertXmltoJson(fileContent);

            
        return await Task.FromResult(_jsonFileUtils.SerializeToUtf8Bytes(jsonContent));
    }
}