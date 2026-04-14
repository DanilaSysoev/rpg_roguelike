using System;

namespace RpgRoguelike.Core.Effects;

public class Stun : IEffect
{
    public bool IsActive => time > 0;

    public Stun(int time)
    {
        this.time = time;
    }

    public void Apply(Entity target)
    {
        --time;
        target.IsStunned = true;
    }

    public override string ToString()
    {
        return $"Stun: {time} time(s)";
    }

    private int time;
}
