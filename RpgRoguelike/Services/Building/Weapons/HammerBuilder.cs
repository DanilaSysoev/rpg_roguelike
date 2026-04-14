using RpgRoguelike.Core.Weapons;

namespace RpgRoguelike.Services.Building.Weapons;

public class HammerBuilder : WeaponBuilderBase<Hammer>
{
    public HammerBuilder SetStunChance(int stunChance)
    {
        this.stunChance = stunChance;
        return this;
    }
    
    public HammerBuilder SetStunTime(int stunTime)
    {
        this.stunTime = stunTime;
        return this;
    }

    public override Hammer Build()
    {
        var res = base.Build();

        res.StunChance = stunChance;
        res.StunTime = stunTime;

        return res;
    }

    private int stunChance = DEFAULT_STUN_CHANCE;
    private int stunTime = DEFAULT_STUN_TIME;

    private const int DEFAULT_STUN_CHANCE = 20;
    private const int DEFAULT_STUN_TIME = 1;
}
