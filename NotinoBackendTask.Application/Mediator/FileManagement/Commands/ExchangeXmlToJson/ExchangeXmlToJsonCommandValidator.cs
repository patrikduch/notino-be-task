//-----------------------------------------------------------------------------------
// <copyright file="ExchangeXmlToJsonCommandValidator.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeXmlToJson;

using FluentValidation;
using NotinoBackendTask.Application.Enums;

/// <summary>
/// Validator of <seealso cref="ExchangeXmlToJsonCommandRequest"/> object.
/// </summary>
public class ExchangeXmlToJsonCommandValidator : AbstractValidator<ExchangeXmlToJsonCommandRequest>
{
    public ExchangeXmlToJsonCommandValidator()
    {
        RuleFor(x => x.File)
             .NotEmpty().WithMessage("{PropertyName} is required.")
             .NotNull().WithMessage("{PropertyName} cannot be null.")
             .MustAsync((processedFile, token) =>
             {
                 if (processedFile is null)
                 {
                     return Task.FromResult(false);
                 }

                 var extension = Path.GetExtension(processedFile.FileName);

                 if (extension.Equals(FileExtensions.XML))
                 {
                     return Task.FromResult(true);
                 }

                 return Task.FromResult(false);
             })
             .WithMessage("Filename must have .XML extension.");
    }
}
