namespace NotinoBackendTask.Application.Mediator.FileManagement.Queries;

using MediatR;
using System.Xml.Linq;

public class LoadXmlFileQueryRequest : IRequest<XDocument>
{
    public string XmlContent { get; set; } = string.Empty;
}
