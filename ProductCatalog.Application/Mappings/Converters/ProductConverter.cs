using AutoMapper;
using ProductCatalog.Application.Features.Models.DataAccessObjects;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Mappings.Converters;

public class ProductConverter : ITypeConverter<ProductDao, Product>
{
    public Product Convert(ProductDao source, Product destination, ResolutionContext context)
    {
        var id = source.Id;
        var price = source.Price;
        var code = source.Code;
        var name = source.Name;

        return Product.Load(id, name, code, price);
    }
}