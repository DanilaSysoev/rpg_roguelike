using RpgRoguelike.Core;
using RpgRoguelike.Core.Control;
using RpgRoguelike.Services.Base;
using RpgRoguelike.Services.Building.Entities;
using RpgRoguelike.Services.Building.Rooms;
using RpgRoguelike.Services.Building.Weapons;

namespace RpgRoguelike;

public class Game
{
    public Player Player => player;
    public Room Room => room;

    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
            }
            return instance;
        }
    }

    public void Init(IRoomBuilder roomBuilder)
    {
        room = roomBuilder.Build();

        drawBuffer = new char[room.Height, room.Width];
        gameStopped = false;

        ConfigureCommands();
        Console.Clear();
    }

    public static void Cleanup() { instance = null; }

    public void Run()
    {
        while (!GemeIsEnded())
        {
            Render();
            HandleInput();
            Update();
        }
    }

    private bool GemeIsEnded()
    {
        return gameStopped || !player.IsAlive;
    }

    private void HandleInput()
    {
        var key = Console.ReadKey(true);

        if (commands.ContainsKey(key.Key))
            commands[key.Key].Execute();
    }

    private void Update()
    {
        Cell curr = room.GetCell(player.Position);
        foreach(var reward in curr.Rewards)
            reward.Get(player);

        curr.RemoveAllRewards();

        room.Update();
        player.Update();
    }

    private void Render()
    {
        Console.Clear();
        ClearDrawBuffer();
        DrawRoom();
        DrawPlayer();
        DrawEnemies();
        Flush();
        DrawStatus();
    }

    private void DrawStatus()
    {
        Console.WriteLine($"Health: {player.Health} / {player.MaxHealth}");
        foreach(var effect in player.Effects)
            Console.WriteLine($"\t{effect}");
        Console.WriteLine($"Gold: {player.Gold}");
        foreach (var enemy in room.Enemies)
        {
            Console.WriteLine($"Enemy Health: {enemy.Health} / {enemy.MaxHealth}");
            foreach(var effect in enemy.Effects)
                Console.WriteLine($"\t{effect}");
        }
    }
    
    private Game()
    {
        var hammer = new HammerBuilder().SetStunChance(30)
                                        .SetStunTime(1)
                                        .SetName("Hammer")
                                        .SetDamage(20)
                                        .Build();
        player = new PlayerBuilder().SetMaxHealth(100)
                                    .SetHealth(50)
                                    .SetPosition(new Position(5, 5))
                                    .SetWeapon(hammer)
                                    .SetName("Player")
                                    .Build();
        gameStopped = false;
    }

    private void ClearDrawBuffer()
    {
        for(int line = 0; line < room.Height; ++line)
        {
            for(int column = 0; column < room.Width; ++column)
            {
                drawBuffer[line, column] = ' ';
            }
        }
    }

    private void DrawRoom()
    {
        foreach(var cell in room.Cells)
        {
            if (!cell.IsPassable)
                drawBuffer[cell.Position.Line, cell.Position.Column] = '#';
            else if(cell.Rewards.Any())
                drawBuffer[cell.Position.Line, cell.Position.Column] = '*';
        }
    }

    private void DrawPlayer()
    {
        drawBuffer[player.Position.Line, player.Position.Column] = '@';
    }

    private void DrawEnemies()
    {
        foreach(var enemy in room.Enemies)
            drawBuffer[enemy.Position.Line, enemy.Position.Column] = enemy.Name[0];
    }

    private void Flush()
    {
        for(int line = 0; line < room.Height; ++line)
        {
            for(int column = 0; column < room.Width; ++column)
            {
                if(line < Console.BufferHeight && column < Console.BufferWidth - 1)
                    Console.Write(drawBuffer[line, column]);
            }
            Console.WriteLine();
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

        exitCommand.OnExecute += ExitCommandHandler;
        moveUpCommand.OnExecute += player.MotionCommandHandler;
        moveDownCommand.OnExecute += player.MotionCommandHandler;
        moveLeftCommand.OnExecute += player.MotionCommandHandler;
        moveRightCommand.OnExecute += player.MotionCommandHandler;
        
        commands.Add(ConsoleKey.Escape, exitCommand);
        commands.Add(ConsoleKey.UpArrow, moveUpCommand);
        commands.Add(ConsoleKey.DownArrow, moveDownCommand);
        commands.Add(ConsoleKey.LeftArrow, moveLeftCommand);
        commands.Add(ConsoleKey.RightArrow, moveRightCommand);
        commands.Add(ConsoleKey.W, moveUpCommand);
        commands.Add(ConsoleKey.S, moveDownCommand);
        commands.Add(ConsoleKey.A, moveLeftCommand);
        commands.Add(ConsoleKey.D, moveRightCommand);
    }

    private void ExitCommandHandler(CommandData data)
    {
        gameStopped = true;
    }

    private bool gameStopped;
    private static Game? instance;

    private char[,] drawBuffer = null!;
    private Room room = null!;
    private readonly Player player;

    private readonly Dictionary<ConsoleKey, ICommand> commands = new();
}
