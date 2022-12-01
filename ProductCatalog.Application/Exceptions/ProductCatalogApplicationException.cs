using System.Net;

namespace ProductCatalog.Application.Exceptions;

public class ProductCatalogApplicationException : Exception
{
    public string Title { get; }
    public HttpStatusCode StatusCode { get; }


    public ProductCatalogApplicationException(string messageDetail, string title, HttpStatusCode statusCode) : base(
        messageDetail)
    {
        Title = title;
        StatusCode = statusCode;
    }
}