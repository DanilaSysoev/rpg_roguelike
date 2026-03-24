using RpgRoguelike.Core.Effects;
using RpgRoguelike.Services.RandomGeneration;

namespace RpgRoguelike.Core.Weapons.Charms;

public class FireCharms : CharmBase
{
    public int FireDamage { get; internal set; }
    public int BurningChance { get; internal set; }
    public int BurningTime { get; internal set; }
    public int BurningPower { get; internal set; }

    protected override void AdditionalAttack(Entity target)
    {
        target.TakeDamage(FireDamage);
    }

    protected override void AdditionalEffect(Entity target)
    {
        if(RandomService.RandomProvider.CheckChance(BurningChance))
            target.AddEffect(new Burning(BurningPower, BurningTime));
    }
}
