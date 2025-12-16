namespace ApiProject.Services;

public class RandomService
{
    private readonly Random _random = new Random();

    public virtual int Get()
    {
        return _random.Next(1, 11); // Returns random number between 1 and 10 (inclusive)
    }
}
