//-------------------------------------------------------------------------------
// <copyright file="EmailUtils.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-------------------------------------------------------------------------------

namespace NotinoBackendTask.Infrastructure.Helpers.Email;

using Microsoft.Extensions.Options;
using NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;
using NotinoBackendTask.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

/// <summary>
/// Utilities for sending e-mails.
/// </summary>
public class EmailUtils : IEmailUtils
{
    private readonly EmailSettings _emailSettings;

    /// <summary>
    /// Initializes a new instance of the <seealso cref="EmailUtils"/> class.
    /// </summary>
    /// <param name="emailSettings">Email settings configuration.</param>
    public EmailUtils(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    /// <summary>
    /// Send single e-mail.
    /// </summary>
    /// <param name="email"><seealso cref="EmailModel"/> data object.</param>
    /// <returns>Bool with success indication.</returns>
    public async Task<bool> SendEmail(EmailModel email)
    {
        var apiKey = _emailSettings.ApiKey;
        var client = new SendGridClient("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");
        var from = new EmailAddress("duchpatrik@icloud.com", "Bc. Patrik Duch");
        var subject = "Sending with SendGrid is Fun";
        var to = new EmailAddress("duchpatrik@icloud.com", "Bc. Patrik Duch");
        var plainTextContent = "and easy to do anywemhere, even with C#";
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);

        return response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted;
    }
}