using RpgRoguelike.Core;
using RpgRoguelike.Core.Weapons;
using RpgRoguelike.Exceptions;
using RpgRoguelike.Services.Base;

namespace RpgRoguelike.Services.Building.Entities;

public class EntityBuilderBase<T> : IEntityBuilder<T> where T : Entity, new()
{
    public EntityBuilderBase()
    {
        health = maxHealth;
    }

    public EntityBuilderBase<T> SetHealth(int health)
    {
        this.health = health;
        return this;
    }
    public EntityBuilderBase<T> SetMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        return this;
    }
    public EntityBuilderBase<T> SetName(string name)
    {
        this.name = name;
        return this;
    }
    public EntityBuilderBase<T> SetPosition(Position position)
    {
        this.position = position;
        return this;
    }
    public EntityBuilderBase<T> SetWeapon(IWeapon? weapon)
    {
        this.weapon = weapon;
        return this;
    }

    public virtual T Build()
    {
        if(name is null)
            throw new BuildingProcessException(
                "Error while entity building. Property `Name` is not setupped."
            );
        if(maxHealth < 1)
            throw new BuildingProcessException(
                "Error while entity building. " +
                "Property `MaxHealth` must be greater than 0"
            );

        var result = new T();
        result.MaxHealth = maxHealth;
        result.Name = name;
        result.Position = position;
        result.Health = health;
        result.Weapon = weapon;

        return result;
    }


    private int health;
    private int maxHealth = DEFAULT_MAX_HEALTH;
    private string? name;
    private Position position = DEFAULT_POSITION;
    private IWeapon? weapon;


    private const int DEFAULT_MAX_HEALTH = 100;
    private static readonly Position DEFAULT_POSITION = new Position(0, 0);
}
