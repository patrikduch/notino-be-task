//--------------------------------------------------------------------------------
// <copyright file="ExchangeXmlToJsonUseCaseTest.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------------

namespace NotinoBackendTask.Tests.UseCases.FileManagement.Commands;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeXmlToJson;
using NotinoBackendTask.Application.UseCases.FileManagement.Commands;

/// <summary>
/// Unit test for <seealso cref="ExchangeXmlToJsonUseCase"/>.
/// </summary>
public class ExchangeXmlToJsonUseCaseTest
{
    private ExchangeXmlToJsonUseCase _exchangeXmlToJsonUseCase;
    private Mock<IFileUtils> _fileUtils;
    private Mock<ILogger<ExchangeXmlToJsonUseCase>> _logger;
    private Mock<IJsonFileUtils> _jsonFileUtils;
    private Mock<IXmlFileUtils> _xmlFileUtils;

    public ExchangeXmlToJsonUseCaseTest()
    {
        _fileUtils = new Mock<IFileUtils>();
        _logger = new Mock<ILogger<ExchangeXmlToJsonUseCase>>();
        _jsonFileUtils = new Mock<IJsonFileUtils>();
        _xmlFileUtils = new Mock<IXmlFileUtils>();
        _exchangeXmlToJsonUseCase = new ExchangeXmlToJsonUseCase(_fileUtils.Object, _logger.Object, _jsonFileUtils.Object, _xmlFileUtils.Object);
    }

    /// <summary>
    /// Test the exchange xml to json.
    /// </summary>
    [Fact]
    public async Task ExchangeXmlToJson_Success()
    {
        #region Arrange

        //Setup mock file using a memory stream
        var content = "Fake file";
        var fileName = "test.xml";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);

        _fileUtils.Setup(f => f.LoadFile(file)).Returns("{\"title\":\"Test\"}");

        #endregion


        #region Act
        var actual = await _exchangeXmlToJsonUseCase.Handle(new ExchangeXmlToJsonCommandRequest
        {
            File = file,
        }, CancellationToken.None);
        #endregion


        #region Assert
        actual.Should().NotBeNull();
        actual.IsSuccess.Should().BeTrue();
        #endregion
    }


    [Fact]
    public async Task EchangeJsonToXml_InvalidFile_FaultedFlow()
    {
        #region Act

        var actual = await _exchangeXmlToJsonUseCase.Handle(new ExchangeXmlToJsonCommandRequest
        {
            File = null,
        }, CancellationToken.None);

        #endregion

        #region Assert
        actual.Should().NotBeNull();
        actual.IsFaulted.Should().BeTrue();
        #endregion
    }


    [Fact]
    public async Task ExchangeXmlToJson_InvalidFileExtension_FaultedFlow()
    {
        #region Arrange
        //Setup mock file using a memory stream
        var content = "Fake file";
        var fileName = "test.text";
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(content);
        writer.Flush();
        stream.Position = 0;

        //create FormFile with desired data
        IFormFile file = new FormFile(stream, 0, stream.Length, "id_from_form", fileName);
        #endregion


        #region Act
        var actual = await _exchangeXmlToJsonUseCase.Handle(new ExchangeXmlToJsonCommandRequest
        {
            File = file,
        }, CancellationToken.None);

        #endregion

        #region Assert
        actual.Should().NotBeNull();
        actual.IsFaulted.Should().BeTrue();
        #endregion
    }
}
