using ProductCatalog.Domain.Base;

namespace ProductCatalog.Domain.Entities;

public class Product : Entity
{
    public string Name { get; }
    public string Code { get; }
    public decimal Price { get; private set; }

    private Product(string name, decimal price)
    {
        Name = name;
        Price = price;
        Code = Guid.NewGuid().ToString();
    }

    private Product(int id, string name, string code, decimal price) : base(id)
    {
        Name = name;
        Code = code;
        Price = price;
    }

    public static Product Create(string name, decimal price)
    {
        return new Product(name, price);
    }

    public static Product Load(int id, string name, string code, decimal price)
    {
        return new Product(id ,name, code, price);
    } 
}