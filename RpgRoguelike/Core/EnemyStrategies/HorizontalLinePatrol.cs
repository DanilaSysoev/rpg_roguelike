using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Core.EnemyStrategies;

public class HorizontalLinePatrol : IMovementStrategy
{
    public HorizontalLinePatrol(
        int leftBorder,
        int rightBorder,
        Direction direction = Direction.Right
    )
    {
        this.leftBorder = leftBorder;
        this.rightBorder = rightBorder;
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

    private bool IsOnLeftBorder(Enemy enemy)
    {
        return enemy.Position.Column == leftBorder;
    }

    private bool IsOnRightBorder(Enemy enemy)
    {
        return enemy.Position.Column == rightBorder;
    }

    private Direction direction;
    private readonly int leftBorder;
    private readonly int rightBorder;
}
