using RpgRoguelike.Gameplay;

namespace RpgRoguelike.Core;

public class Cell
{
    public Position Position { get; private set; }
    public bool IsPassable { get; private set; }

    public IEnumerable<IReward> Rewards => rewards;
    
    public Cell(Position position, bool isPassable)
    {
        Position = position;
        IsPassable = isPassable;
    }

    public void AddReward(IReward reward)
    {
        rewards.Add(reward);
    }

    public void RemoveReward(IReward reward)
    {
        rewards.Remove(reward);
    }

    private readonly List<IReward> rewards = new();
}
