namespace RpgRoguelike.Core.Control;

public class Command : ICommand
{
    public event CommandDelegate? OnExecute;

    public void Execute() => OnExecute?.Invoke(BuildData());

    public virtual CommandData BuildData() { return new CommandData(); }
}
