namespace RpgRoguelike.Core.Effects;

public class Stun : IEffect
{
    public bool IsActive => time >= 0;

    public Stun(int time)
    {
        this.time = time;
    }

    public void Apply(Entity target)
    {
        if (time == 0) target.IsStunned = false;
        else target.IsStunned = true;
        --time;
    }

    public override string ToString()
    {
        return $"Stun: {time} time(s)";
    }

    private int time;
}
