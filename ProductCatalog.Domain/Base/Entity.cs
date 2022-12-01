namespace ProductCatalog.Domain.Base;

public abstract class Entity
{
    protected int Id { get; }
    protected Entity()
    { 
    }

    protected Entity(int id)
    {
        Id = id;
    }
}