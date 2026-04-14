using RpgRoguelike.Core.Weapons.Charms;

namespace RpgRoguelike.Services.Building.Charms;

public class FireCharmBuilder : CharmBaseBuilder<FireCharms>
{
    public FireCharmBuilder SetFireDamage(int fireDamage)
    {
        this.fireDamage = fireDamage;
        return this;
    }
    public FireCharmBuilder SetBurningChance(int burningChance)
    {
        this.burningChance = burningChance;
        return this;
    }
    public FireCharmBuilder SetBurningTime(int burningTime)
    {
        this.burningTime = burningTime;
        return this;
    }
    public FireCharmBuilder SetBurningPower(int burningPower)
    {
        this.burningPower = burningPower;
        return this;
    }

    public override FireCharms Build()
    {
        FireCharms res = base.Build();

        res.BurningChance = burningChance;
        res.BurningPower = burningPower;
        res.BurningTime = burningTime;
        res.FireDamage = fireDamage;

        return res;
    }

    private int fireDamage = DEFAULT_FIRE_DAMAGE;
    private int burningChance = DEFAULT_FIRE_CHANCE;
    private int burningTime = DEFAULT_BURNING_TIME; 
    private int burningPower = DEFAULT_BURNING_POWER;

    private const int DEFAULT_FIRE_DAMAGE = 5;
    private const int DEFAULT_FIRE_CHANCE = 10;
    private const int DEFAULT_BURNING_TIME = 3;
    private const int DEFAULT_BURNING_POWER = 5;
}
