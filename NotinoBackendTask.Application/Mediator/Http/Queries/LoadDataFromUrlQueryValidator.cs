//-----------------------------------------------------------------------------------
// <copyright file="LoadDataFromUrlQueryValidator.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Mediator.Http.Queries;

using FluentValidation;
using System.Text.RegularExpressions;


/// <summary>
/// Validator of Mediator <seealso cref="LoadDataFromUrlQueryRequest"/> object.
/// </summary>
public class LoadDataFromUrlQueryValidator : AbstractValidator<LoadDataFromUrlQueryRequest>
{
    public LoadDataFromUrlQueryValidator()
    {
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull().WithMessage("{PropertyName} cannot be null.");

        RuleFor(x => x.Url)
            .MustAsync((processedUrl, token) =>
            {
                string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
                Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                var isValid = Rgx.IsMatch(processedUrl);

                return Task.FromResult(isValid);
            });
    }
}
