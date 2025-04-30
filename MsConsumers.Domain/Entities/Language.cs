namespace MsConsumers.Domain.Entities;

public class Language
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }

    // Navigation properties
    public virtual ICollection<Consumer> Consumers { get; private set; }

    protected Language() { }

    public Language(string code, string description)
    {
        Id = Guid.NewGuid();
        Code = code;
        Description = description;
        Consumers = new List<Consumer>();
    }

    public void Update(string code, string description)
    {
        Code = code;
        Description = description;
    }
} 