using RpgRoguelike.Core.Weapons;
using RpgRoguelike.Core.Weapons.Charms;
using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Services.Building.Charms;

public class CharmBaseBuilder<T> : ICharmBuilder<T> where T : CharmBase, new()
{
    public CharmBaseBuilder<T> SetWeapon(IWeapon weapon)
    {
        this.weapon = weapon;
        return this;
    }

    public virtual T Build()
    {
        if(weapon is null)
            throw new BuildingProcessException(
                "Error during charm building. Property `Weapon` is not setupped"
            );
        
        return new T { Weapon = weapon };
    }

    private IWeapon? weapon;
}
