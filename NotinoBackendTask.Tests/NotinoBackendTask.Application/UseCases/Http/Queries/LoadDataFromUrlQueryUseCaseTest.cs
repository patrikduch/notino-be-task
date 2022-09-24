//------------------------------------------------------------------------------
// <copyright file="LoadDataFromUrlQueryUseCaseTest.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Tests.UseCases.Http.Queries;

using FluentAssertions;
using Moq;
using Moq.Protected;
using NotinoBackendTask.Application.Mediator.Http.Queries;
using NotinoBackendTask.Application.UseCases.Http.Queries;
using System.Net;

/// <summary>
/// Unit tests for <seealso cref="LoadDataFromUrlQueryUseCase"/>.
/// </summary>
public class LoadDataFromUrlQueryUseCaseTest
{
    private readonly LoadDataFromUrlQueryUseCase _loadDataFromUrlQueryUseCase;
    private readonly Mock<IHttpClientFactory> _httpClientFactory = new();
    private readonly Mock<HttpClient> _httpClient = new();
    private readonly Mock<HttpMessageHandler> _handlerMock = new();

    public LoadDataFromUrlQueryUseCaseTest()
    {
        var response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent("document data"),
        };

        _handlerMock
           .Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(response);

        _httpClient = new Mock<HttpClient>(_handlerMock.Object);
        _httpClientFactory.Setup(h => h.CreateClient("httpClient")).Returns(_httpClient.Object);

        
        _loadDataFromUrlQueryUseCase = new LoadDataFromUrlQueryUseCase(_httpClientFactory.Object);
    }

    /// <summary>
    /// Unit test for loading data from invalid HTTP url.
    /// </summary>
    [Fact]
    public async Task LoadDataFromUrl_WithInvalidUrl_IsFaulted ()
    {
        #region Arrange
        var url = string.Empty;
        #endregion

        #region Act
        var actual = await _loadDataFromUrlQueryUseCase.Handle(new LoadDataFromUrlQueryRequest
        {
            Url = url
        }, CancellationToken.None);
        #endregion
        
        #region Assert
        actual.IsFaulted.Should().BeTrue();
        #endregion
    }

    /// <summary>
    /// Unit test for loading data with valid HTTP url.
    /// </summary>
    [Fact]
    public async Task LoadDataFromUrl_WithValidUrl_WithSuccess()
    {
        #region Arrange
        var url = "http://aukro.cz";
        #endregion

        #region Act
        var actual = await _loadDataFromUrlQueryUseCase.Handle(new LoadDataFromUrlQueryRequest
        {
            Url = url
        }, CancellationToken.None);
        #endregion

        #region Assert
        actual.Should().Be("document data");
        #endregion
    }
}
