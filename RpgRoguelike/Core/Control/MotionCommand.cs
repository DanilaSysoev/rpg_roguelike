namespace RpgRoguelike.Core.Control;

public class MotionCommand : Command
{
    public Direction Direction { get; set; }

    public override CommandData BuildData()
    {
        return new CommandData { Direction = Direction };
    }
}
