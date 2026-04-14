using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Core.EnemyStrategies;

public class HorizontalLinePatrol : IMovementStrategy
{
    public HorizontalLinePatrol(
        Direction direction = Direction.Right
    )
    {
        this.direction = direction;
    }

    public void Move(Enemy enemy)
    {
        if(IsOnRightBorder(enemy))
            direction = Direction.Left;
        else if(IsOnLeftBorder(enemy))
            direction = Direction.Right;

        var newPosition = direction switch
        {
            Direction.Right => enemy.Position.RightNeighbor(),
            Direction.Left => enemy.Position.LeftNeighbor(),
            _ => throw new InvalidDirectionException(direction)
        };

        if(Game.Instance.Room.CanMoveTo(newPosition))
            enemy.Position = newPosition;
    }

    private static bool IsOnLeftBorder(Enemy enemy)
    {
        return enemy.Position.Column == 1;
    }

    private static bool IsOnRightBorder(Enemy enemy)
    {
        return enemy.Position.Column == Game.Instance.MapWidth - 2;
    }

    private Direction direction;
}
