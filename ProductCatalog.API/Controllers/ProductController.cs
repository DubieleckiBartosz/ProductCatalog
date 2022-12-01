using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ProductCatalog.Application.Wrappers;
using ProductCatalog.Application.Parameters.ProductParameters;
using ProductCatalog.Application.Features.Product.Queries.GetProductsBySearch;
using ProductCatalog.Application.Features.Product.Commands.AddProduct;

namespace ProductCatalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : BaseController
{
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType( 204)]
    [SwaggerOperation(Summary = "Add new product")]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddNewProduct([FromBody] AddProductCommandParameters parameters)
    {
        var command = AddProductCommand.Create(parameters);
        await Mediator.Send(command);
        return NoContent();
    }

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 404)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Response<>), 200)]
    [SwaggerOperation(Summary = "Get products by search")]
    [HttpPost("[action]")]
    public async Task<IActionResult> GetProductsBySearch([FromBody] GetProductsBySearchQueryParameters parameters)
    {
        var query = GetProductsBySearchQuery.Create(parameters);
        var resultQuery = await Mediator.Send(query);

        return Ok(resultQuery);
    }
}