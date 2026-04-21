using RpgRoguelike.Core.Control;

namespace RpgRoguelike.Core;

public class Player : Entity
{
    public int Gold { get; set; }

    public void MotionCommandHandler(CommandData data)
    {
        Room room = Game.Instance.Room;
        switch (data.Direction)
        {
            case Direction.Up:
                if (room.CanMoveTo(Position.UpNeighbor()))
                    Move(Direction.Up);
                break;
            case Direction.Right:
                if(room.CanMoveTo(Position.RightNeighbor()))
                    Move(Direction.Right);
                break;
            case Direction.Down:
                if (room.CanMoveTo(Position.DownNeighbor()))
                    Move(Direction.Down);
                break;
            case Direction.Left:
                if (room.CanMoveTo(Position.LeftNeighbor()))
                    Move(Direction.Left);
                break;
        }
    }
}
