using Newtonsoft.Json;

namespace ProductCatalog.Application.Parameters.ProductParameters; 

public class AddProductCommandParameters
{ 
    public string Name { get; init; }
    public decimal Price { get; init; }

    public AddProductCommandParameters()
    {
    }

    [JsonConstructor]
    public AddProductCommandParameters(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}