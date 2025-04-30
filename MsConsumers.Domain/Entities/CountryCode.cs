using System.Collections.Generic;

namespace MsConsumers.Domain.Entities;

public class CountryCode
{
    public Guid Id { get; private set; }
    public string Code { get; private set; } = string.Empty;
    public string CountryName { get; private set; } = string.Empty;
    public ICollection<Consumer> Consumers { get; private set; } = new List<Consumer>();
    public ICollection<ConsumerAddress> Addresses { get; private set; } = new List<ConsumerAddress>();

    private CountryCode() { }

    public CountryCode(Guid id, string code, string countryName)
    {
        Id = id;
        Code = code;
        CountryName = countryName;
    }

    public void Update(string code, string countryName)
    {
        Code = code;
        CountryName = countryName;
    }
} 