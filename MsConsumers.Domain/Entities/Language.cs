using System.Collections.Generic;

namespace MsConsumers.Domain.Entities;

public class Language
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public ICollection<Consumer> Consumers { get; private set; } = new List<Consumer>();

    private Language() { }

    public Language(Guid id, string code, string description)
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