using RpgRoguelike.Core;

namespace RpgRoguelike.Services.Building.Entities;

public class PlayerBuilder : EntityBuilderBase<Player>
{
    public PlayerBuilder SetGold(int gold)
    {
        this.gold = gold;
        return this;
    }

    public override Player Build()
    {
        var res = base.Build();
        res.Gold = gold;
        return res;
    }
 
    private int gold = DEFAULT_GOLD;

    private const int DEFAULT_GOLD = 0;
}
