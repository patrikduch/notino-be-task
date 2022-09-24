//------------------------------------------------------------------------------
// <copyright file="ExchangeJsonToXmlUseCaseTest.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Tests.UseCases.FileManagement.Commands;

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Moq;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeJsonToXml;
using NotinoBackendTask.Application.UseCases.FileManagement.Commands;

public class ExchangeJsonToXmlUseCaseTest
{
    private ExchangeJsonToXmlUseCase _exchangeJsonToXmlUseCase;
    private Mock<IFileUtils> _fileUtils;

    public ExchangeJsonToXmlUseCaseTest()
    {
        _fileUtils = new Mock<IFileUtils>();
        _exchangeJsonToXmlUseCase = new ExchangeJsonToXmlUseCase(_fileUtils.Object);
    }

    /// <summary>
    /// Test the exchange xml to json.
    /// </summary>
    [Fact]
    public async Task ExchangeJsonToXml_ValidData_SuccessFlow()
    {
        #region Arrange

        //Setup mock file using a memory stream
        var content = "Fake file";
        var fileName = "test.json";
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
        var actual = await _exchangeJsonToXmlUseCase.Handle(new ExchangeJsonToXmlCommandRequest
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
    public async Task ExchangeJsonToXml_InvalidFile_FaultedFlow()
    {
        #region Arrange
        //Setup mock file using a memory stream
        var content = "Fake file";
        var fileName = "test.json";
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

        var actual = await _exchangeJsonToXmlUseCase.Handle(new ExchangeJsonToXmlCommandRequest
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
    public async Task ExchangeJsonToXml_InvalidFileExtension_FaultedFlow()
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
        #endregion


        #region Act
        var actual = await _exchangeJsonToXmlUseCase.Handle(new ExchangeJsonToXmlCommandRequest
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