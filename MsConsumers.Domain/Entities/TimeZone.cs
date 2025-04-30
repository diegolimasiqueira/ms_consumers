using System.Collections.Generic;

namespace MsConsumers.Domain.Entities;

public class TimeZone
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ICollection<Consumer> Consumers { get; private set; } = new List<Consumer>();

    private TimeZone() { }

    public TimeZone(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
} 