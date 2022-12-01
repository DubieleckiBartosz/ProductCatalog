using ProductCatalog.Application.Abstractions;
using ProductCatalog.Application.Features.Models.SharedModels;
using ProductCatalog.Application.Parameters.ProductParameters;
using ProductCatalog.Application.Parameters.SearchParameters;
using ProductCatalog.Application.Search;
using ProductCatalog.Application.Wrappers;

namespace ProductCatalog.Application.Features.Product.Queries.GetProductsBySearch;

public record GetProductsBySearchQuery
    (SortModel SortModel, string? Code, decimal? Price, string? Name, int PageNumber, int PageSize) : BaseSearchQuery(
            PageNumber, PageSize),
        IQuery<Response<List<ProductViewModel>>>
{ 
    
    public static GetProductsBySearchQuery Create(GetProductsBySearchQueryParameters parameters)
    {
        parameters ??= new GetProductsBySearchQueryParameters();
        if (parameters is {SortModelParameters: null})
        {
            parameters.SortModelParameters = new SortModelParameters();
        }

        var sortType = string.IsNullOrWhiteSpace(parameters.SortModelParameters.Type) ? "desc" : parameters.SortModelParameters.Type;
        var sortName = string.IsNullOrWhiteSpace(parameters.SortModelParameters.Name) ? "Id" : parameters.SortModelParameters.Name;
        sortType = sortType.ToLower() == "desc" ? "desc" : "asc";

        var sortModel = new SortModel(sortType, sortName);

        return new GetProductsBySearchQuery(sortModel, parameters.Code, parameters.Price, parameters.Name,
            parameters.PageNumber, parameters.PageSize);
    }
}