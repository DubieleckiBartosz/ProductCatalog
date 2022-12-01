using System.Net;

namespace ProductCatalog.Application.Exceptions;

public class BadRequestException : ProductCatalogApplicationException
{
    public BadRequestException(string messageDetail, string title) : base(messageDetail, title, HttpStatusCode.BadRequest)
    {
    }
}