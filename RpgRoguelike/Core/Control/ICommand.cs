namespace RpgRoguelike.Core.Control;

public struct CommandData
{
    public Direction Direction { get; set; }
}

public delegate void CommandDelegate(CommandData data);

public interface ICommand
{
    public event CommandDelegate OnExecute;

    void Execute();
}
