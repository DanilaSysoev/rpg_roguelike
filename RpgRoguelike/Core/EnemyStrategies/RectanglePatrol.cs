using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Core.EnemyStrategies;

public enum ClockwiseDirection
{
    Clockwise,
    CounterClockwise
}

public class RectanglePatrol : IMovementStrategy
{
    public RectanglePatrol(
        Position topLeftCorner,
        Position bottomRightCorner,
        Direction direction = Direction.Right,
        ClockwiseDirection clockwiseDirection = ClockwiseDirection.Clockwise
    )
    {
        this.direction = direction;
        this.clockwiseDirection = clockwiseDirection;
        this.topLeftCorner = topLeftCorner;
        this.bottomRightCorner = bottomRightCorner;
    }

    public void Move(Enemy enemy)
    {
        EnemyPositionValidation(enemy);

        if(IsOnTopLeftCorner(enemy))
            direction = clockwiseDirection == ClockwiseDirection.Clockwise
                      ? Direction.Right
                      : Direction.Down;
        else if(IsOnTopRightCorner(enemy))
            direction = clockwiseDirection == ClockwiseDirection.Clockwise
                      ? Direction.Down
                      : Direction.Left;
        else if(IsOnBottomRightCorner(enemy))
            direction = clockwiseDirection == ClockwiseDirection.Clockwise
                      ? Direction.Left
                      : Direction.Up;
        else if(IsOnBottomLeftCorner(enemy))
            direction = clockwiseDirection == ClockwiseDirection.Clockwise
                      ? Direction.Up
                      : Direction.Right;

        var newPosition = direction switch
        {
            Direction.Right => enemy.Position.RightNeighbor(),
            Direction.Left => enemy.Position.LeftNeighbor(),
            Direction.Up => enemy.Position.UpNeighbor(),
            Direction.Down => enemy.Position.DownNeighbor(),
            _ => throw new InvalidDirectionException(direction)
        };

        if(Game.Instance.Room.CanMoveTo(newPosition))
            enemy.Position = newPosition;
    }

    private bool IsOnBottomLeftCorner(Enemy enemy)
    {
        return enemy.Position ==
            new Position(bottomRightCorner.Line, topLeftCorner.Column);
    }

    private bool IsOnBottomRightCorner(Enemy enemy)
    {
        return enemy.Position == bottomRightCorner;
    }

    private bool IsOnTopRightCorner(Enemy enemy)
    {
        return enemy.Position ==
            new Position(topLeftCorner.Line, bottomRightCorner.Column);
    }

    private bool IsOnTopLeftCorner(Enemy enemy)
    {
        return enemy.Position == topLeftCorner;
    }

    private void EnemyPositionValidation(Enemy enemy)
    {
        if(enemy.Position.Line != topLeftCorner.Line &&
           enemy.Position.Line != bottomRightCorner.Line &&
           enemy.Position.Column != topLeftCorner.Column &&
           enemy.Position.Column != bottomRightCorner.Column)
        {
            throw new ArgumentException(
                $"Enemy's position must be on the rectangle. Pos={enemy.Position}, TL={topLeftCorner}, BR={bottomRightCorner}"
            );
        }
    }

    private Direction direction;
    private readonly ClockwiseDirection clockwiseDirection;
    private readonly Position topLeftCorner;
    private readonly Position bottomRightCorner;
}
