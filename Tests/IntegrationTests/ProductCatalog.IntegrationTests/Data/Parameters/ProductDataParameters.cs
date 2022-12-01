using Bogus; 
using ProductCatalog.Application.Parameters.ProductParameters;
using ProductCatalog.Application.Parameters.SearchParameters;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.IntegrationTests.Data.Parameters;

internal class ProductDataParameters
{
    private const string Locale = "pl";
    public static AddProductCommandParameters GetAddProductCommandParametersModel()
    { 
        var product = GetProductFaker().Generate();

        return product;
    }

    public static GetProductsBySearchQueryParameters? GetEmptyProductsBySearchQueryParametersModel()
    {
        return new GetProductsBySearchQueryParameters();
    }

    public static List<Product> GetProductList()
    {
        var parameters = GetProductFaker().Generate(10);
        var products = parameters.Select(_ => Product.Create(_.Name, _.Price)).ToList();
        return products;
    }


    public static GetProductsBySearchQueryParameters GetProductsBySearchQueryParametersSimpleModel()
    {
        var sortModelParameters = new SortModelParameters("asc", "Price");

        var search = new GetProductsBySearchQueryParameters(sortModelParameters, null, null, null, 3, 10);

        return search;
    }

    private static Faker<AddProductCommandParameters> GetProductFaker() =>
        new Faker<AddProductCommandParameters>(Locale)
            .RuleFor(_ => _.Name, q => q.Commerce.ProductName())
            .RuleFor(_ => _.Price,
                q => decimal.TryParse(q.Commerce.Price(), out var value) ? value : new Random().Next(1, 1000));
}