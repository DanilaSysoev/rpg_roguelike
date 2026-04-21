using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Core.EnemyStrategies;


public class VerticalLinePatrol : IMovementStrategy
{
    public VerticalLinePatrol(
        int topBorder,
        int bottomBorder,
        Direction direction = Direction.Down
    )
    {
        this.topBorder = topBorder;
        this.bottomBorder = bottomBorder;
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

    private bool IsOnTopBorder(Enemy enemy)
    {
        return enemy.Position.Line == topBorder;
    }

    private bool IsOnBottomBorder(Enemy enemy)
    {
        return enemy.Position.Line == bottomBorder;
    }

    private Direction direction;
    private readonly int topBorder;
    private readonly int bottomBorder;
}
