using RpgRoguelike.Gameplay;
using RpgRoguelike.Services.Base;

namespace RpgRoguelike.Services;

public class RandomHealthRewardCreator : IRewardCreator
{
    public RandomHealthRewardCreator(int minValue, int maxValue, Random random)
    {
        this.minValue = minValue;
        this.maxValue = maxValue;
        this.random = random;
    }

    public IReward Create()
    {
        int value = random.Next(minValue, maxValue + 1);
        return new HealthReward(value);
    }

    private readonly int minValue;
    private readonly int maxValue;
    private readonly Random random;
}
