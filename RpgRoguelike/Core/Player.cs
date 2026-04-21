using RpgRoguelike.Core.Control;

namespace RpgRoguelike.Core;

public class Player : Entity
{
    public int Gold { get; set; }
    public Position NextPosition { get; set; }

    public void MotionCommandHandler(CommandData data)
    {
        switch (data.Direction)
        {
            case Direction.Up:
                NextPosition = Position.UpNeighbor();
                break;
            case Direction.Right:
                NextPosition = Position.RightNeighbor();
                break;
            case Direction.Down:
                NextPosition = Position.DownNeighbor();
                break;
            case Direction.Left:
                NextPosition = Position.LeftNeighbor();
                break;
        }
    }

    public void StayCommandHandler(CommandData data)
    {
        NextPosition = Position;
    }

    public override void Update()
    {
        base.Update();

        Room room = Game.Instance.Room;
        if (room.CanMoveTo(NextPosition))
            Position = NextPosition;
        else
        {
            Enemy? enemy = Game.Instance.Room.GetEnemyAt(NextPosition);
            if (enemy is not null)
                Attack(enemy);
        }
        NextPosition = Position;
    }
}
