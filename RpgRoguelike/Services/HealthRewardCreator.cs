using RpgRoguelike.Gameplay;
using RpgRoguelike.Services.Base;

namespace RpgRoguelike.Services;

public class HealthRewardCreator : IRewardCreator
{
    public HealthRewardCreator(int value)
    {
        this.value = value;
    }

    public IReward Create()
    {
        return new HealthReward(value);
    }

    private readonly int value;
}
