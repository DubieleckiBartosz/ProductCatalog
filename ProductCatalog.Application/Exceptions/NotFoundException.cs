using System.Net;

namespace ProductCatalog.Application.Exceptions;

public class NotFoundException : ProductCatalogApplicationException
{
    public NotFoundException(string messageDetail, string title) : base(messageDetail, title,
        HttpStatusCode.NotFound)
    {
    }
}