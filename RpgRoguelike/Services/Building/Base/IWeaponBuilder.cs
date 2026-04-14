using RpgRoguelike.Core.Weapons;

namespace RpgRoguelike.Services.Base;

public interface IWeaponBuilder<out T> where T : Weapon, new()
{
    T Build();
}
