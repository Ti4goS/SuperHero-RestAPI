namespace SuperHeroAPI;

public class SuperHero
{
    
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public String LastName { get; set; } = String.Empty;
    public String City { get; set; } = String.Empty;

    public static implicit operator ValueTask<object>(SuperHero v)
    {
        throw new NotImplementedException();
    }
}

