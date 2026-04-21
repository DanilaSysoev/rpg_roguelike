using RpgRoguelike.Core.Control;

namespace RpgRoguelike.Core;

public class Player : Entity
{
    public int Gold { get; set; }
    public Position NextPosition { get; set; }

    public void MotionCommandHandler(CommandData data)
    {
        Room room = Game.Instance.Room;
        switch (data.Direction)
        {
            case Direction.Up:
                if (room.CanMoveTo(Position.UpNeighbor()))
                    NextPosition = Position.UpNeighbor();
                break;
            case Direction.Right:
                if(room.CanMoveTo(Position.RightNeighbor()))
                    NextPosition = Position.RightNeighbor();
                break;
            case Direction.Down:
                if (room.CanMoveTo(Position.DownNeighbor()))
                    NextPosition = Position.DownNeighbor();
                break;
            case Direction.Left:
                if (room.CanMoveTo(Position.LeftNeighbor()))
                    NextPosition = Position.LeftNeighbor();
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        Position = NextPosition;
        NextPosition = Position;
    }
}
