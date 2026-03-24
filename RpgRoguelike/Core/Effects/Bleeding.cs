namespace RpgRoguelike.Core.Effects;

public class Bleeding : IEffect
{
    public bool IsActive => time > 0;

    public Bleeding(int value, int time)
    {
        this.value = value;
        this.time = time;
    }

    public void Apply(Entity target)
    {
        target.Health -= value;
        --time;
    }

    public override string ToString()
    {
        return $"Bleeding: {value} for {time} time(s)";
    }

    private readonly int value;
    private int time;
}
