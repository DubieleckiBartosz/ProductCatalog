namespace ProductCatalog.Application.Features.Models.DataAccessObjects;

public class ProductDao
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}