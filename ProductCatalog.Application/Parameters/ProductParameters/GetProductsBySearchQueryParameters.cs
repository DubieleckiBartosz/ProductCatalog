using Newtonsoft.Json;
using ProductCatalog.Application.Parameters.SearchParameters;

namespace ProductCatalog.Application.Parameters.ProductParameters;

public class GetProductsBySearchQueryParameters : BaseSearchQueryParameters
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public decimal? Price { get; init; }
    public SortModelParameters? SortModelParameters { get; set; }

    public GetProductsBySearchQueryParameters()
    {
    }

    [JsonConstructor]
    public GetProductsBySearchQueryParameters(SortModelParameters? sortModelParameters, string? code, string? name,
        decimal? price, int pageNumber = 1,
        int pageSize = 100)
    {
        Code = code;
        Name = name;
        Price = price;
        PageNumber = pageNumber;
        PageSize = pageSize;
        SortModelParameters = sortModelParameters;
    }
}