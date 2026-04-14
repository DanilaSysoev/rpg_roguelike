using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Core.EnemyStrategies;


public class VerticalLinePatrol : IMovementStrategy
{
    public VerticalLinePatrol(
        Direction direction = Direction.Down
    )
    {
        this.direction = direction;
    }

    public void Move(Enemy enemy)
    {
        if(IsOnBottomBorder(enemy))
            direction = Direction.Up;
        else if(IsOnTopBorder(enemy))
            direction = Direction.Down;
        var newPosition = direction switch
        {
            Direction.Down => enemy.Position.DownNeighbor(),
            Direction.Up => enemy.Position.UpNeighbor(),
            _ => throw new InvalidDirectionException(direction)
        };

        if(Game.Instance.Room.CanMoveTo(newPosition))
            enemy.Position = newPosition;
    }

    private static bool IsOnTopBorder(Enemy enemy)
    {
        return enemy.Position.Line == 1;
    }

    private static bool IsOnBottomBorder(Enemy enemy)
    {
        return enemy.Position.Line == Game.Instance.MapHeight - 2;
    }

    private Direction direction;
}
