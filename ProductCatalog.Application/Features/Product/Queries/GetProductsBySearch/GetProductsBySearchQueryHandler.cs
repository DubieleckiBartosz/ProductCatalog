using AutoMapper;
using ProductCatalog.Application.Abstractions;
using ProductCatalog.Application.Features.Models.SharedModels;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Application.Wrappers;

namespace ProductCatalog.Application.Features.Product.Queries.GetProductsBySearch;

public class GetProductsBySearchQueryHandler : IQueryHandler<GetProductsBySearchQuery, Response<List<ProductViewModel>>>
{ 
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductsBySearchQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    public async Task<Response<List<ProductViewModel>>> Handle(GetProductsBySearchQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetProductsBySearchAsync(request.Code, request.Name, request.Price,
            request.SortModel.Type, request.SortModel.Name, request.PageNumber, request.PageSize);

        var resultMap = _mapper.Map<List<Domain.Entities.Product>>(result);

        var response = resultMap.Select(_ => new ProductViewModel(_.Code, _.Name, _.Price)).ToList();

        return Response<List<ProductViewModel>>.Ok(response, null);
    }
}