//---------------------------------------------------------------------------------
// <copyright file="EmailSettings.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//---------------------------------------------------------------------------------

namespace NotinoBackendTask.Application.Models;

/// <summary>
/// Email settings model for sending single e-mail.
/// </summary>
public class EmailSettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string FromAddress { get; set; } = string.Empty;

    public string FromName { get; set; } = string.Empty;
}
