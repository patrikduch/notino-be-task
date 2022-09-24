//--------------------------------------------------------------------------------
// <copyright file="SaveLocalFileCommandUseCaseTest.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------------

namespace NotinoBackendTask.Tests.UseCases.FileManagement.Commands;

using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Application.Dtos;
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.SaveLocalFile;
using NotinoBackendTask.Application.UseCases.FileManagement.Commands;


/// <summary>
/// Unit tests for <seealso cref="SaveLocalFileCommandUseCase"/>.
/// </summary>
public class SaveLocalFileCommandUseCaseTest
{
    private SaveLocalFileCommandUseCase _saveLocalFileCommandUseCase;
    private Mock<IFileUtils> _fileUtils;
    private Mock<IJsonFileUtils> _jsonFileUtils;
    private Mock<ILogger<SaveLocalFileCommandUseCase>> _logger;
    private Mock<IXmlFileUtils> _xmlFileUtils;


    public SaveLocalFileCommandUseCaseTest()
    {
        _fileUtils = new Mock<IFileUtils>();
        _jsonFileUtils = new Mock<IJsonFileUtils>();
        _logger = new Mock<ILogger<SaveLocalFileCommandUseCase>>();
        _xmlFileUtils = new Mock<IXmlFileUtils>();


        _saveLocalFileCommandUseCase = new SaveLocalFileCommandUseCase(_fileUtils.Object, _jsonFileUtils.Object, _logger.Object, _xmlFileUtils.Object);
    }

    [Fact]
    public async Task SaveLocalFile_WithJSON_SuccessFlow()
    {

        #region Arrange
        _fileUtils.Setup(f => f.GetExtension("fake path")).Returns(".json");

        _jsonFileUtils.Setup(x => x.WriteLocal(It.IsAny<string>(), It.IsAny<DocumentDto>()))
            .Returns("{\"Title\":\"Test title\",\"Text\":\"Bc.Patrik Duch \"}");
        #endregion

        #region Act

        var actual = await _saveLocalFileCommandUseCase.Handle(new SaveLocalFileCommandRequest
        {
            AbsoluteDestinationFilePath = "fake path",
            Document = new DocumentDto
            {
                Text = "Bc. Patrik Duch",
                Title = "Test title"
            }
        }, CancellationToken.None);

        #endregion

        #region Assert
        actual.Should().NotBeNull();
        actual.IsSuccess.Should().BeTrue();
        #endregion
    }

    [Fact]
    public async Task SaveLocalFile_WithXML_SuccessFlow()
    {

        #region Arrange
        _fileUtils.Setup(f => f.GetExtension("fake path")).Returns(".xml");

        _xmlFileUtils.Setup(x => x.WriteLocal(It.IsAny<string>(), It.IsAny<DocumentDto>()))
            .Returns("<Title>Test title</Title><Text>Bc. Patrik Duch</Text></DocumentDto>");
        #endregion

        #region Act

        var actual = await _saveLocalFileCommandUseCase.Handle(new SaveLocalFileCommandRequest
        {
            AbsoluteDestinationFilePath = "fake path",
            Document = new DocumentDto
            {
                Text = "Bc. Patrik Duch",
                Title = "Test title"
            }
        }, CancellationToken.None);

        #endregion

        #region Assert
        actual.Should().NotBeNull();
        actual.IsSuccess.Should().BeTrue();
        #endregion
    }



    [Fact]
    public async Task SaveLocalFile_InvalidAbsoluteDestinationPath_FaultedFlow()
    {
        #region Act

        var actual = await _saveLocalFileCommandUseCase.Handle(new SaveLocalFileCommandRequest
        {
            AbsoluteDestinationFilePath = string.Empty,
            Document = new Application.Dtos.DocumentDto
            {
                Text = string.Empty,
                Title = string.Empty
            }
        }, CancellationToken.None);

        #endregion

        #region Assert
        actual.Should().NotBeNull();
        actual.IsFaulted.Should().BeTrue();
        #endregion
    }

    [Fact]
    public async Task SaveLocalFile_InvalidDocumentObject_FaultedFlow()
    {
        #region Act

        var actual = await _saveLocalFileCommandUseCase.Handle(new SaveLocalFileCommandRequest
        {
            AbsoluteDestinationFilePath = "fake path",
            Document = null
        }, CancellationToken.None);

        #endregion

        #region Assert
        actual.Should().NotBeNull();
        actual.IsFaulted.Should().BeTrue();
        #endregion
    }
}
