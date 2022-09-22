//-----------------------------------------------------------------------
// <copyright file="IEmailSender.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace NotinoBackendTask.Application.Contracts.Infrastructure.Helpers;

using NotinoBackendTask.Application.Models;

/// <summary>
/// Contract for Email utility.
/// </summary>
public interface IEmailUtils
{
    Task<bool> SendEmail(EmailModel email);
}
