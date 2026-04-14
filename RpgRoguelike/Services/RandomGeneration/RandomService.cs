namespace RpgRoguelike.Services.RandomGeneration;

public static class RandomService
{
    public static IRandomProvider RandomProvider
    {
        get
        {
            if(randomProvider is null)
                randomProvider = new RandomProvider();
            return randomProvider;
        }
        set
        {
            randomProvider = value;
        }
    }

    private static IRandomProvider? randomProvider;
}
