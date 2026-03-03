using RpgRoguelike.Core;

namespace RpgRoguelike.Gameplay;

public class HealthReward : IReward
{
    public int Value { get; private set; }

    public HealthReward(int value) 
    {
        if (value < 1)
            throw new ArgumentException("Gold reward must be greater than 0");

        Value = value;
    }

    public void Get(Player player)
    {
        player.Health += Value;
    }
}
