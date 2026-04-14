using RpgRoguelike.Core.Effects;

namespace RpgRoguelike.Core.Weapons;

public class Dagger : Weapon
{
    public int BleedingValue { get; internal set; }
    public int BleedingTime { get; internal set; }

    public override void ApplyEffect(Entity target)
    {
        target.AddEffect(new Bleeding(BleedingValue, BleedingTime));
    }
}
