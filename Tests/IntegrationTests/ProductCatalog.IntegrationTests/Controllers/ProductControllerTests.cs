using Microsoft.AspNetCore.Mvc.Testing;
using ProductCatalog.Application.Features.Models.SharedModels;
using ProductCatalog.Application.Wrappers;
using ProductCatalog.IntegrationTests.Data.Parameters;
using ProductCatalog.IntegrationTests.Setup;

namespace ProductCatalog.IntegrationTests.Controllers;

public class ProductControllerTests : BaseSetup
{
    private const string AddProductPath = "api/Product/AddNewProduct";
    private const string SearchProductsPath = "api/Product/GetProductsBySearch";
    public ProductControllerTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_Add_New_Product()
    {
        var product = ProductDataParameters.GetAddProductCommandParametersModel();
        var responseMessage = await this.ClientCall(product, HttpMethod.Post, AddProductPath);
         
        Assert.True(responseMessage.IsSuccessStatusCode); 
    }

    [Fact]
    public async Task Should_Return_Data_By_Default_Search()
    {
        var defaultSize = 10;

        var search = ProductDataParameters.GetEmptyProductsBySearchQueryParametersModel();
        var responseMessage = await this.ClientCall(search, HttpMethod.Post, SearchProductsPath);
        var responseData = await this.ReadFromResponse<Response<List<ProductViewModel>>>(responseMessage);

        Assert.True(responseData!.Success);
        Assert.Equal(responseData.Data.Count, defaultSize);
    }
    
    [Fact]
    public async Task Should_Return_Data_By_Simple_Search()
    {
        var defaultSize = 10;

        var search = ProductDataParameters.GetProductsBySearchQueryParametersSimpleModel();

        var responseMessage = await this.ClientCall(search, HttpMethod.Post, SearchProductsPath);
        var responseData = await this.ReadFromResponse<Response<List<ProductViewModel>>>(responseMessage);

        var listResponse = responseData?.Data;

        var result = !listResponse
            ?.Zip(listResponse?.Skip(1) ?? Array.Empty<ProductViewModel>(), (a, b) => a.Price <= b.Price)
            .Contains(false);

        Assert.True(responseData!.Success);
        Assert.Equal(listResponse?.Count, defaultSize);  
        Assert.True(result);
    }
}