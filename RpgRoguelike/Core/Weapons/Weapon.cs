using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Core.Weapons;

public abstract class Weapon : IWeapon
{
    public int Damage { get; internal set; }
    public string Name { get; internal set; } = "";

    public abstract void ApplyEffect(Entity target);
    
    public void Attack(Entity target)
    {
        target.TakeDamage(Damage);
    }

    public override string ToString()
    {
        return Name;
    }

    private protected Weapon() {}
}
