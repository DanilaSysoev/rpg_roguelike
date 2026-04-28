using RpgRoguelike.Core.Control;
using RpgRoguelike.Core.Drawing;

namespace RpgRoguelike.Core.GameStates;

class GameplayState : GameState
{
    public GameplayState(IDrawBuffer drawBuffer) : base(drawBuffer)
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
    {
        Player.Update();

        Cell curr = Room.GetCell(Player.Position);
        foreach(var reward in curr.Rewards)
            reward.Get(Player);

        curr.RemoveAllRewards();

        Room.Update();
    }

    protected override void PrepareDrawBuffer()
    {
        DrawRoom();
        DrawPlayer();
        DrawEnemies();
        DrawStatus();
    }

    private void DrawRoom()
    {
        foreach(var cell in Room.Cells)
        {
            if (!cell.IsPassable)
                DrawBuffer.Add(cell.Position.Column, cell.Position.Line, '#');
            else if(cell.Rewards.Any())
                DrawBuffer.Add(cell.Position.Column, cell.Position.Line, '*');
        }
    }

    private void DrawPlayer()
    {
        DrawBuffer.Add(
            Player.Position.Column,
            Player.Position.Line,
            '@'
        );
    }

    private void DrawEnemies()
    {
        foreach(var enemy in Room.Enemies)
        {
            DrawBuffer.Add(
                enemy.Position.Column, enemy.Position.Line, enemy.Name[0]
            );
        }
    }

    private void DrawStatus()
    {
        DrawBuffer.Add(
            0,
            Room.Height,
            $"Health: {Player.Health} / {Player.MaxHealth}"
        );
        int line = 1;
        foreach(var effect in Player.Effects)
            DrawBuffer.Add(0, Room.Height + line++, $"\t{effect}");
        DrawBuffer.Add(0, Room.Height + line++, $"Gold: {Player.Gold}");
        foreach (var enemy in Room.Enemies)
        {
            DrawBuffer.Add(
                0,
                Room.Height + line++,
                $"Enemy Health: {enemy.Health} / {enemy.MaxHealth}"
            );
            foreach(var effect in enemy.Effects)
                DrawBuffer.Add(0, Room.Height + line++, $"\t{effect}");
        }
    }

    private void ConfigureCommands()
    {
        ICommand exitCommand = new Command();
        ICommand moveUpCommand =
            new MotionCommand { Direction = Direction.Up };
        ICommand moveDownCommand =
            new MotionCommand { Direction = Direction.Down };
        ICommand moveLeftCommand =
            new MotionCommand { Direction = Direction.Left };
        ICommand moveRightCommand = new
            MotionCommand { Direction = Direction.Right };
        ICommand stayCommand = new Command();

        exitCommand.OnExecute += ExitCommandHandler;
        moveUpCommand.OnExecute += Player.MotionCommandHandler;
        moveDownCommand.OnExecute += Player.MotionCommandHandler;
        moveLeftCommand.OnExecute += Player.MotionCommandHandler;
        moveRightCommand.OnExecute += Player.MotionCommandHandler;
        stayCommand.OnExecute += Player.StayCommandHandler;
        
        commands.Add(ConsoleKey.Escape, exitCommand);
        commands.Add(ConsoleKey.UpArrow, moveUpCommand);
        commands.Add(ConsoleKey.DownArrow, moveDownCommand);
        commands.Add(ConsoleKey.LeftArrow, moveLeftCommand);
        commands.Add(ConsoleKey.RightArrow, moveRightCommand);
        commands.Add(ConsoleKey.W, moveUpCommand);
        commands.Add(ConsoleKey.S, moveDownCommand);
        commands.Add(ConsoleKey.A, moveLeftCommand);
        commands.Add(ConsoleKey.D, moveRightCommand);
        commands.Add(ConsoleKey.Spacebar, stayCommand);
    }

    private static void ExitCommandHandler(CommandData data)
    {
        Game.Instance.Stop();
    }

    private static Room Room => Game.Instance.Room;
    private static Player Player => Game.Instance.Player;

    private readonly Dictionary<ConsoleKey, ICommand> commands = new();
}
