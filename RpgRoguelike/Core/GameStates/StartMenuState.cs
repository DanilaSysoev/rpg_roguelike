using RpgRoguelike.Core.Control;
using RpgRoguelike.Core.Drawing;

namespace RpgRoguelike.Core.GameStates;

public class StartMenuState : GameState
{
    public StartMenuState(IDrawBuffer drawBuffer) : base(drawBuffer)
    {
        ConfigureCommands();
    }

    public override void HandleInput()
    {
        var key = Console.ReadKey(true);

        if (commands.ContainsKey(key.Key))
            commands[key.Key].Execute();
    }

    public override void Update()
    {}

    protected override void PrepareDrawBuffer()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            string menuLine = menuItems[i];
            if (cursorPosition == i)
                menuLine = LeftCursor + menuLine + RightCursor;

            DrawBuffer.Add(
                ScreenWidth / 2 - menuLine.Length / 2,
                ScreenHeight / 2 - menuItems.Count / 2 + i,
                menuLine
            );
        }
        string line = new string('-', ScreenWidth - 2);
        DrawBuffer.Add(0, 0, "+" + line + "+");
        DrawBuffer.Add(0, ScreenHeight - 1, "+" + line + "+");
        for (int i = 1; i < ScreenHeight - 1; i++)
        {
            DrawBuffer.Add(0, i, '|');
            DrawBuffer.Add(ScreenWidth - 1, i, '|');
        }
    }

    private void ConfigureCommands()
    {
        ICommand selectCommand = new Command();
        ICommand moveUpCommand =
            new MotionCommand { Direction = Direction.Up };
        ICommand moveDownCommand =
            new MotionCommand { Direction = Direction.Down };

        selectCommand.OnExecute += SelectCommandHandler;
        moveUpCommand.OnExecute += MoveCursorUpHandler;
        moveDownCommand.OnExecute += MoveCursorDownHandler;

        commands.Add(ConsoleKey.Enter, selectCommand);
        commands.Add(ConsoleKey.UpArrow, moveUpCommand);
        commands.Add(ConsoleKey.DownArrow, moveDownCommand);
        commands.Add(ConsoleKey.W, moveUpCommand);
        commands.Add(ConsoleKey.S, moveDownCommand);
    }

    private void MoveCursorDownHandler(CommandData data)
    {
        cursorPosition++;
        if (cursorPosition >= menuItems.Count)
            cursorPosition = 0;
    }

    private void MoveCursorUpHandler(CommandData data)
    {
        cursorPosition--;
        if (cursorPosition < 0)
            cursorPosition = menuItems.Count - 1;
    }

    private void SelectCommandHandler(CommandData data)
    {
        switch (menuItems[cursorPosition])
        {
            case StartGame:
                Game.Instance.State =
                    new GameplayState(DrawBuffer);
                break;
            case Exit:
                Game.Instance.Stop();
                break;
        }
    }

    private readonly Dictionary<ConsoleKey, ICommand> commands = new();

    private readonly List<string> menuItems = new() { StartGame, Exit };
    private int cursorPosition = 0;

    private const string LeftCursor = "> ";
    private const string RightCursor = " <";
    private const string StartGame = "Start Game";
    private const string Exit = "Exit";

    private const int ScreenWidth = 80;
    private const int ScreenHeight = 12;
}