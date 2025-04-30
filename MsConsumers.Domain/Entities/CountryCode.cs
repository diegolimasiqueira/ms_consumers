namespace MsConsumers.Domain.Entities;

public class CountryCode
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public string CountryName { get; private set; }

    // Navigation properties
    public virtual ICollection<Consumer> Consumers { get; private set; }
    public virtual ICollection<ConsumerAddress> Addresses { get; private set; }

    protected CountryCode() { }

    public CountryCode(string code, string countryName)
    {
        Id = Guid.NewGuid();
        Code = code;
        CountryName = countryName;
        Consumers = new List<Consumer>();
        Addresses = new List<ConsumerAddress>();
    }

    public void Update(string code, string countryName)
    {
        Code = code;
        CountryName = countryName;
    }
} 