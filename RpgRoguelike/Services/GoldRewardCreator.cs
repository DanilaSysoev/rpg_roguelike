using RpgRoguelike.Gameplay;

namespace RpgRoguelike.Services;

public class GoldRewardCreator : IRewardCreator
{
    public GoldRewardCreator(int value)
    {
        this.value = value;
    }

    public IReward Create()
    {
        return new GoldReward(value);
    }

    private readonly int value;
}
