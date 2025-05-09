using System.Collections.Generic;

namespace MSConsumers.Domain.Entities;

public class Currency
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ICollection<ConsumerEntity> Consumers { get; private set; } = new List<ConsumerEntity>();

    private Currency() { }

    public Currency(Guid id, string code, string description)
    {
        Id = id;
        Code = code;
        Description = description;
    }

    public void Update(string code, string description)
    {
        Code = code;
        Description = description;
    }
} 