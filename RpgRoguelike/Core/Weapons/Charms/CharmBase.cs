namespace RpgRoguelike.Core.Weapons.Charms;

public abstract class CharmBase : IWeapon
{
    internal IWeapon Weapon { get; set; } = null!;

    public void Attack(Entity target)
    {
        Weapon.Attack(target);
        AdditionalAttack(target);
    }

    public void ApplyEffect(Entity target)
    {
        Weapon.ApplyEffect(target);
        AdditionalEffect(target);
    }

    protected abstract void AdditionalAttack(Entity target);
    protected abstract void AdditionalEffect(Entity target);
}
