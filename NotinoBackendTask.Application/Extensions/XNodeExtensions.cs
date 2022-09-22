//-----------------------------------------------------------------------
// <copyright file="XNodeExtensions.cs" website="Patrikduch.com">
//     Copyright (c) Patrik Duch, IČ: 09225471
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace NotinoBackendTask.Application.Extensions;

using System.Text;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Extension of <seealso cref="XNode"/> object.
/// </summary>
public static class XNodeExtensions
{
    static Encoding DefaultEncoding { get; } = new UTF8Encoding(false); // Disable the BOM because XElement.ToString() does not include it.

    /// <summary>
    /// Encode XML string to byte array.
    /// </summary>
    /// <param name="node">XML entrypoint node.</param>
    /// <param name="options">Custom option that will be used during conversion.</param>
    /// <param name="encoding">Type of conversion encoding.</param>
    /// <returns>Byte array.</returns>
    public static byte[] PrepareByteArray(this XNode node, SaveOptions options = default, Encoding encoding = default)
    {
        // Emulate the settings of XElement.ToString() and XDocument.ToString()
        // https://referencesource.microsoft.com/#System.Xml.Linq/System/Xml/Linq/XLinq.cs,2004
        // I omitted the XML declaration because XElement.ToString() omits it, but you might want to include it, depending upon your needs.
        var settings = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = (options & SaveOptions.DisableFormatting) == 0, Encoding = encoding ?? DefaultEncoding };
        if ((options & SaveOptions.OmitDuplicateNamespaces) != 0)
            settings.NamespaceHandling |= NamespaceHandling.OmitDuplicates;
        return node.ToByteArray(settings);
    }

    /// <summary>
    /// Convert encoded string to byte array.
    /// </summary>
    /// <param name="node">XML node.</param>
    /// <param name="settings">Settings for writing to XML file.</param>
    /// <returns>Byte array.</returns>
    public static byte[] ToByteArray(this XNode node, XmlWriterSettings settings)
    {
        using var ms = new MemoryStream();
        using (var writer = XmlWriter.Create(ms, settings))
            node.WriteTo(writer);
        return ms.ToArray();
    }
}
