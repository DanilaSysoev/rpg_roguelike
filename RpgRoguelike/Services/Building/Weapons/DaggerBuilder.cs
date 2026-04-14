using RpgRoguelike.Core.Weapons;

namespace RpgRoguelike.Services.Building.Weapons;

public class DaggerBuilder : WeaponBuilderBase<Dagger>
{
    public DaggerBuilder SetBleeding(int value, int time)
    {
        bleedingTime = time;
        bleedingValue = value;

        return this;
    }

    public DaggerBuilder SetBleedingValue(int value)
    {
        bleedingValue = value;

        return this;
    }

    public DaggerBuilder SetBleedingTime(int time)
    {
        bleedingTime = time;

        return this;
    }

    public override Dagger Build()
    {
        Dagger res = base.Build();

        res.BleedingTime = bleedingTime;
        res.BleedingValue = bleedingValue;

        return res;
    }

    private int bleedingValue = DEFAULT_BLEEDING_VALUE;
    private int bleedingTime = DEFAULT_BLEEDING_TIME;

    private const int DEFAULT_BLEEDING_VALUE = 1;
    private const int DEFAULT_BLEEDING_TIME = 3;
}
