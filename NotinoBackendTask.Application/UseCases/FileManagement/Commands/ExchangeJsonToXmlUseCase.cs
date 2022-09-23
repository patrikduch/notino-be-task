//---------------------------------------------------------------------------------
// <copyright file="ExchangeJsonToXmlUseCase.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.UseCases.FileManagement.Commands;

using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;
using MediatR;
using Newtonsoft.Json;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Application.Extensions;
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeJsonToXml;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

/// <summary>
/// Bussiness logic for conversion of JSON file to XML.
/// </summary>
public class ExchangeJsonToXmlUseCase : IRequestHandler<ExchangeJsonToXmlCommandRequest, Result<byte[]>>
{
    private readonly IFileUtils _fileUtils;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="ExchangeJsonToXmlUseCase"/> class.
    /// </summary>
    /// <param name="fileUtils"><seealso cref="IFileUtils"/> dependency object for accessing local file storage.</param>
    public ExchangeJsonToXmlUseCase(IFileUtils fileUtils)
    {
        _fileUtils = fileUtils;
    }

    /// <summary>
    /// Interceptor for handling file conversion from JSON to XML fileformat.
    /// </summary>
    /// <param name="request"><seealso cref="ExchangeJsonToXmlCommandRequest"/> interceptor object.</param>
    /// <param name="cancellationToken"><seealso cref="CancellationToken"/> object.</param>
    /// <returns></returns>
    public async Task<Result<byte[]>> Handle(ExchangeJsonToXmlCommandRequest request, CancellationToken cancellationToken)
    {
        var exchangeJsonToXmlCommandValidator = new ExchangeJsonToXmlCommandValidator();
        var validationResult = await exchangeJsonToXmlCommandValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var validationException = new ValidationException(validationResult.Errors);
            return new Result<byte[]>(validationException);
        }

        if (request.File is null)
        {
            var fileIsNullFailure = new ValidationResult { 
                Errors =  new List<ValidationFailure>
                {
                    new ValidationFailure
                    {
                        ErrorCode = "400",
                        ErrorMessage = "File cannot be null"
                    }
                }
            };

            var validationException = new ValidationException(fileIsNullFailure.Errors);
            return new Result<byte[]>(validationException);
        }


        var fileContent = _fileUtils.LoadFile(request.File);

        var json = JsonConvert.DeserializeObject(fileContent).ToString();
        XNode node = JsonConvert.DeserializeXNode(json);

        var bytes = node.PrepareByteArray();

        return await Task.FromResult(bytes);
    }
}
