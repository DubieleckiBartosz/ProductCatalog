using ProductCatalog.Application.Features.Models.DataAccessObjects;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<ProductDao?> GetProductByNameAsync(string name);
    Task<List<ProductDao>> GetProductsBySearchAsync(string? code, string? name, decimal? price, string sortModelType,
        string sortModelName, int pageNumber, int pageSize);
}