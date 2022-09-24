//-----------------------------------------------------------------------------------
// <copyright file="SaveLocalFileCommandValidator.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.FileManagement.Commands.SaveLocalFile;

using FluentValidation;

/// <summary>
/// Validator of Mediator <seealso cref="SaveLocalFileCommandRequest"/> object.
/// </summary>
public class SaveLocalFileCommandValidator : AbstractValidator<SaveLocalFileCommandRequest>
{

    public SaveLocalFileCommandValidator()
    {
        RuleFor(x => x.AbsoluteDestinationFilePath)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().WithMessage("{PropertyName} cannot be null.");

        RuleFor(x => x.Document)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull().WithMessage("{PropertyName} cannot be null.");
    }   
}
