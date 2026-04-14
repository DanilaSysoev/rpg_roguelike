using RpgRoguelike.Core;
using RpgRoguelike.Core.EnemyStrategies;

namespace RpgRoguelike.Services.Building.Entities;

public class EnemyBuilder : EntityBuilderBase<Enemy>
{
    public EnemyBuilder SetMovementStrategy(IMovementStrategy movementStrategy)
    {
        this.movementStrategy = movementStrategy;
        return this;
    }

    public override Enemy Build()
    {
        var result = base.Build();
        result.MovementStrategy = movementStrategy;
        return result;
    }

    private IMovementStrategy? movementStrategy;
}
