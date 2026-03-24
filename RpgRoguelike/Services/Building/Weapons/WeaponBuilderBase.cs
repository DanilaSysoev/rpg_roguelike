using RpgRoguelike.Core.Weapons;
using RpgRoguelike.Services.Base;
using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Services.Building.Weapons;

public class WeaponBuilderBase<T> : IWeaponBuilder<T> where T : Weapon, new()
{
    public WeaponBuilderBase<T> SetDamage(int damage)
    {
        this.damage = damage;
        return this;
    }

    public WeaponBuilderBase<T> SetName(string name)
    {
        this.name = name;
        return this;
    }

    public virtual T Build()
    {
        if (damage is null)
            throw new BuildingProcessException(
                "Error during weapon building. Property `Damage` not setupped."
            );
        if (name is null)
            throw new BuildingProcessException(
                "Error during weapon building. Property `Name` not setupped."
            );
        var res = new T();

        res.Damage = damage.Value;
        res.Name = name;

        return res;
    }

    private int? damage;
    private string? name; 
}
