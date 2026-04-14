namespace RpgRoguelike.Core.Effects;

public class Burning : IEffect
{
    public Burning(int power, int time)
    {
        this.power = power;
        this.time = time;
    }

    public bool IsActive => time > 0;

    public void Apply(Entity target)
    {
        target.TakeDamage(power);
        --time;
    }

    public override string ToString()
    {
        return $"Burning: {power} for {time} time(s)";
    }

    private int time;
    private readonly int power;
}
