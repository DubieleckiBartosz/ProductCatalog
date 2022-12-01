using System.Data;
using Dapper;
using Microsoft.Extensions.Options;
using ProductCatalog.Application.Constants;
using ProductCatalog.Application.Exceptions;
using ProductCatalog.Application.Features.Models.DataAccessObjects;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Application.Logging;
using ProductCatalog.Application.Options;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Infrastructure.Repositories;

public class ProductRepository : BaseRepository<ProductRepository>, IProductRepository
{
    public ProductRepository(IOptions<ConnectionStrings> connectionString,
        ILoggerManager<ProductRepository> loggerManager) : base(connectionString, loggerManager)
    {
    }

    public async Task AddAsync(Product product)
    {
        var param = new DynamicParameters();

        param.Add("@price", product.Price);
        param.Add("@code", product.Code);
        param.Add("@name", product.Name);

        var result = await this.ExecuteAsync("product_createNewProduct_I", param, commandType: CommandType.StoredProcedure);
        if (result <= 0)
        {
            throw new InvalidOperationException(Strings.NewProductFailedMessage);
        }
    }
     
    public async Task<ProductDao?> GetProductByNameAsync(string name)
    {
        var param = new DynamicParameters();
        
        param.Add("@name", name);

        var result = await QueryAsync<ProductDao>("product_getProductByName_S", param,
            CommandType.StoredProcedure);

        return result.FirstOrDefault();
    }

    public async Task<List<ProductDao>> GetProductsBySearchAsync(string? code, string? name, decimal? price,
        string sortModelType, string sortModelName, int pageNumber, int pageSize)
    {
        var param = new DynamicParameters();

        param.Add("@price", price);
        param.Add("@code", code);
        param.Add("@name", name);
        param.Add("@sortModelType", sortModelType);
        param.Add("@sortModelName", sortModelName);
        param.Add("@pageNumber", pageNumber);
        param.Add("@pageSize", pageSize);

        var result = (await QueryAsync<ProductDao>("product_getProductsBySearch_S", param,
            CommandType.StoredProcedure))?.ToList();

        if (result == null || !result.Any())
        {
            throw new NotFoundException(Strings.ProductsNotFoundMessage, Strings.ProductsNotFoundTitle);
        }

        return result;
    }
}