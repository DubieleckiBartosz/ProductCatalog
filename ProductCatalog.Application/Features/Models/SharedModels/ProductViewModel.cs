namespace ProductCatalog.Application.Features.Models.SharedModels;

public record ProductViewModel
{
    public string Code { get; }
    public string Name { get; }
    public decimal Price { get; }
    public ProductViewModel(string code, string name, decimal price)
    {
        Code = code;
        Name = name;
        Price = price;
    }
}