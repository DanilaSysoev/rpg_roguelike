using RpgRoguelike.Core.Effects;
using RpgRoguelike.Services.RandomGeneration;

namespace RpgRoguelike.Core.Weapons;

public class Hammer : Weapon
{
    public int StunChance { get; internal set; }
    public int StunTime { get; internal set; }

    public override void ApplyEffect(Entity target)
    {
        if(RandomService.RandomProvider.CheckChance(StunChance))
            target.AddEffect(new Stun(StunTime));
    }
}
