//-----------------------------------------------------------------------------------
// <copyright file="ExchangeJsonToXmlCommandValidator.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeJsonToXml;

using FluentValidation;
using NotinoBackendTask.Application.Enums;

/// <summary>
/// Validator of Mediator <seealso cref="ExchangeJsonToXmlCommandRequest"/> object.
/// </summary>
public class ExchangeJsonToXmlCommandValidator : AbstractValidator<ExchangeJsonToXmlCommandRequest>
{
    public ExchangeJsonToXmlCommandValidator()
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

                if (extension.Equals(FileExtensions.JSON))
                {
                    return Task.FromResult(true);
                }

                return Task.FromResult(false);
            })
            .WithMessage("Filename must have .JSON extension.");
    }
}
