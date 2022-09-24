//-------------------------------------------------------------------------------
// <copyright file="LoadDataFromUrlQueryUseCase.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.UseCases.Http.Queries;

using FluentValidation;
using LanguageExt.Common;
using MediatR;
using NotinoBackendTask.Application.Mediator.Http.Queries;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Bussiness logic for loading data from HTTP Url.
/// </summary>
public class LoadDataFromUrlQueryUseCase : IRequestHandler<LoadDataFromUrlQueryRequest, Result<string>>
{
    private readonly IHttpClientFactory _httpClientFactory;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="LoadDataFromUrlQueryUseCase"/> class.
    /// </summary>
    /// <param name="httpClientFactory"><seealso cref="IHttpClientFactory"/> Factory for accessing HttpClient.</param>
    public LoadDataFromUrlQueryUseCase(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    /// <summary>
    /// Interceptor for loading external file content from HTTP URL.
    /// </summary>
    /// <param name="request"><seealso cref="LoadDataFromUrlQueryRequest"/> interceptor object.</param>
    /// <param name="cancellationToken"><seealso cref="CancellationToken"/> object.</param>
    /// <returns></returns>
    public async Task<Result<string>> Handle(LoadDataFromUrlQueryRequest request, CancellationToken cancellationToken)
    {

        using(var httpClient = _httpClientFactory.CreateClient("httpClient"))
        {
            var createValueItemCommandValidator = new LoadDataFromUrlQueryValidator();
            var validationResult = await createValueItemCommandValidator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                var validationException = new ValidationException(validationResult.Errors);
                return new Result<string>(validationException);
            }

            var httpResponseMessage = await httpClient.GetAsync(request.Url);
 
            return await httpResponseMessage.Content.ReadAsStringAsync();
        }       
    }
}