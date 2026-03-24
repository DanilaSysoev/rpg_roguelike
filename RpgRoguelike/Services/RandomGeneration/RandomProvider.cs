namespace RpgRoguelike.Services.RandomGeneration;

public class RandomProvider : IRandomProvider
{
    public bool CheckChance(int chance)
    {
        return random.Next(100) < chance;
    }

    private readonly Random random = new Random();
}
