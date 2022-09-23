//---------------------------------------------------------------------------
// <copyright file="FormatExchangeController.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IÈ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------

namespace NotinoBackendTask.API.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotinoBackendTask.API.Extensions;
using NotinoBackendTask.Application.Dtos.Requests;
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeJsonToXml;
using NotinoBackendTask.Application.Mediator.FileManagement.Commands.ExchangeXmlToJson;
using NotinoBackendTask.Application.Mediator.FileManagement.Queries;
using NotinoBackendTask.Application.Mediator.Http.Queries;

/// <summary>
/// Rest API controller for managing file exchange between most common API formats.
/// </summary>
[ApiController]
[Route("[controller]")]
public class FormatExchangeController : ControllerBase
{
    private readonly IMediator _mediator;

    public FormatExchangeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Load data from HTTP url.
    /// </summary>
    /// <param name="url">Url of address of source data, that will be fetched.</param>
    /// <returns>Text data after downloading content of particular url.</returns>
    [HttpGet("load-url")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("text/html")]
    public async Task<IActionResult> LoadDataFromUrl([FromQuery] string url)
    {
        return (await _mediator.Send(new LoadDataFromUrlQueryRequest
        {
            Url = url
        })).ToActionResult(r => new ContentResult
        {
            Content = r,
            ContentType = "text/html",
            StatusCode = 200
        });
    }

    /// <summary>
    /// Download file content in Json format.
    /// </summary>
    /// <param name="file">XML file input.</param>
    /// <param name="exchangeXmlToJsonDto"></param>
    /// <returns>Downloadable XML file.</returns>
    [HttpPost("exchange-xml-json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/json")]
    public async Task<IActionResult> ExchangeXMLToJson(IFormFile file, [FromQuery] ExchangeXmlToJsonRequestDto exchangeXmlToJsonDto)
    {
        return (await _mediator.Send(new ExchangeXmlToJsonCommandRequest
        {
          File = file,
        })).ToActionResult(r => File(r, "application/json", exchangeXmlToJsonDto.DestinationFileName));
    }

    /// <summary>
    /// Download file content in Json format.
    /// </summary>
    /// <param name="file">Json file input</param>
    /// <param name="exchangeJsonToXmlDto"></param>
    /// <returns>Downloadable JSON file.</returns>
    [HttpPost("exchange-json-xml")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Produces("application/xml")]
    public async Task<IActionResult> ExchangeJsonToXml(IFormFile file, [FromQuery] ExchangeJsonToXmlRequestDto exchangeJsonToXmlDto)
    {
        return (await _mediator.Send(new ExchangeJsonToXmlCommandRequest
        {
            File = file,
        })).ToActionResult(r => File(r, "application/xml", exchangeJsonToXmlDto.DestinationFileName));
    }

    /// <summary>
    /// Load file from any local resource.
    /// </summary>
    /// <param name="file">Input file name.</param>
    /// <returns>Content of loaded file.</returns>
    [HttpPost("load-file")]
    public async Task<IActionResult> LoadLocalFileFromPath(IFormFile file)
    {
        return (await _mediator.Send(new LoadLocalFileQueryRequest
        {
            File = file,
        })).ToActionResult(r => r);
    }

    [HttpGet("save-file")]
    public async Task<IActionResult> SaveFileToPath(IFormFile file)
    {
        return Ok();
    }

}