using AutoMapper;
using ProductCatalog.Application.Features.Models.DataAccessObjects;
using ProductCatalog.Application.Mappings.Converters;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductDao, Product>().ConvertUsing(new ProductConverter());
    }
}