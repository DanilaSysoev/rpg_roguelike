namespace RpgRoguelike.Core.Weapons;


public interface IWeapon
{
    void Attack(Entity target);
    void ApplyEffect(Entity target);
}
