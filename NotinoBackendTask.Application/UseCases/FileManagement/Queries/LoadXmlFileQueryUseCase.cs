namespace NotinoBackendTask.Application.UseCases.FileManagement.Queries;

using MediatR;
using NotinoBackendTask.Application.Mediator.FileManagement.Queries;
using System.Xml;
using System.Xml.Linq;

public class LoadXmlFileQueryUseCase : IRequestHandler<LoadXmlFileQueryRequest, XDocument>
{
    public Task<XDocument> Handle(LoadXmlFileQueryRequest request, CancellationToken cancellationToken)
    {
        var doc = XDocument.Parse(request.XmlContent);

        return Task.FromResult(doc);

    }
}
