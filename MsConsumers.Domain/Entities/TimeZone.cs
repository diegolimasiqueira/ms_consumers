namespace MsConsumers.Domain.Entities;

public class TimeZone
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }

    // Navigation properties
    public virtual ICollection<Consumer> Consumers { get; private set; }

    protected TimeZone() { }

    public TimeZone(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Consumers = new List<Consumer>();
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
} 