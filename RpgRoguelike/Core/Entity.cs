using RpgRoguelike.Core.Effects;
using RpgRoguelike.Core.Weapons;

namespace RpgRoguelike.Core;

public class Entity
{
    public string Name { get; internal set; } = "";
    public int Health
    { 
        get
        {
            return health;
        }
        set
        {
            health = Math.Min(value, MaxHealth);
        }
    }
    public int MaxHealth { get; internal set; }
    public Position Position { get; set; }
    public bool IsStunned { get; set; }
    public IWeapon? Weapon { get; set; }
    public IReadOnlyList<IEffect> Effects => effects;
    public bool IsAlive => Health > 0;

    public void Move(Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                Position = Position.UpNeighbor();
                break;
            case Direction.Right:
                Position = Position.RightNeighbor();
                break;
            case Direction.Down:
                Position = Position.DownNeighbor();
                break;
            case Direction.Left:
                Position = Position.LeftNeighbor();
                break;
        }
    }

    public virtual void Attack(Entity target)
    {
        if(Weapon is not null)
        {
            Weapon.Attack(target);
            Weapon.ApplyEffect(target);
        }
    }

    public virtual void Update()
    {
        ApplyEffects();
        CleanEffects();
    }

    private void CleanEffects()
    {
        var notActive = effects.Where(e => !e.IsActive).ToList();

        foreach(var effect in notActive)
            effects.Remove(effect);
    }

    private void ApplyEffects()
    {
        foreach(var effect in effects)
            if(effect.IsActive)
                effect.Apply(this);
    }

    public void AddEffect(IEffect effect)
    {
        effects.Add(effect);
    }
    public void RemoveEffect(IEffect effect)
    {
        effects.Remove(effect);
    }

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
    }

    private int health;
    private readonly List<IEffect> effects = new();
}
