using System.Collections.Generic;

namespace MSConsumers.Domain.Entities;

public class TimeZone
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ICollection<ConsumerEntity> Consumers { get; private set; } = new List<ConsumerEntity>();

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