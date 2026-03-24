using RpgRoguelike.Core;
using RpgRoguelike.Services;
using RpgRoguelike.Services.Building.Entities;
using RpgRoguelike.Services.Building.Rooms;
using RpgRoguelike.Services.Building.Weapons;

namespace RpgRoguelike;

public class Game
{
    public int MapWidth { get; private set; }
    public int MapHeigth { get; private set; }
    public Player Player => player;


    public static Game Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Game();
                instance.Init();
            }
            return instance;
        }
    }

    private void Init()
    {
        gameStopped = false;
        Console.Clear();
    }

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
        return gameStopped;
    }

    private void HandleInput()
    {
        var key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.Escape:
                gameStopped = true;
                break;
            case ConsoleKey.UpArrow:
                if (room.CanMoveTo(player.Position.UpNeighbor())
                    && enemy.Position != player.Position.UpNeighbor())
                    player.Move(Direction.Up);
                if (enemy.Position == player.Position.UpNeighbor())
                    player.Attack(enemy);
                break;
            case ConsoleKey.RightArrow:
                if(room.CanMoveTo(player.Position.RightNeighbor())
                   && enemy.Position != player.Position.RightNeighbor())
                    player.Move(Direction.Right);
                if (enemy.Position == player.Position.RightNeighbor())
                    player.Attack(enemy);
                break;
            case ConsoleKey.DownArrow:
                if (room.CanMoveTo(player.Position.DownNeighbor())
                    && enemy.Position != player.Position.DownNeighbor())
                    player.Move(Direction.Down);
                if (enemy.Position == player.Position.DownNeighbor())
                    player.Attack(enemy);
                break;
            case ConsoleKey.LeftArrow:
                if (room.CanMoveTo(player.Position.LeftNeighbor())                
                    && enemy.Position != player.Position.LeftNeighbor())
                    player.Move(Direction.Left);
                if (enemy.Position == player.Position.LeftNeighbor())
                    player.Attack(enemy);
                break;
        }
    }

    private void Update()
    {
        Cell curr = room.GetCell(player.Position);
        foreach(var reward in curr.Rewards)
            reward.Get(player);

        curr.RemoveAllRewards();

        player.Update();
        enemy.Update();
    }

    private void Render()
    {
        Console.Clear();
        ClearDrawBuffer();
        DrawRoom();
        DrawPlayer();
        DrawEnemy();
        Flush();
        DrawStatus();
    }

    private void DrawStatus()
    {
        Console.WriteLine($"Health: {player.Health} / {player.MaxHealth}");
        foreach(var effect in player.Effects)
            Console.WriteLine($"\t{effect}");
        Console.WriteLine($"Gold: {player.Gold}");
        Console.WriteLine($"Enemy Health: {enemy.Health} / {enemy.MaxHealth}");
        foreach(var effect in enemy.Effects)
            Console.WriteLine($"\t{effect}");
    }
    
    private Game()
    {
        MapHeigth = 15;
        MapWidth = 40;

        drawBuffer = new char[MapHeigth, MapWidth];
        room = new RandomRoomBuilder()
            .SetSize(MapWidth, MapHeigth)
            .SetRandomSeed((int)DateTime.Now.Ticks)
            .Build();
        var dagger = new DaggerBuilder().SetBleedingTime(3)
                                        .SetBleedingValue(5)
                                        .SetName("Dagger")
                                        .SetDamage(10)
                                        .Build();
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
        enemy = new EnemyBuilder().SetMaxHealth(100)
                                  .SetHealth(100)
                                  .SetPosition(new Position(7, 7))
                                  .SetWeapon(dagger)
                                  .SetName("Goblin")
                                  .Build();
        gameStopped = false;
    }

    private void ClearDrawBuffer()
    {
        for(int line = 0; line < MapHeigth; ++line)
        {
            for(int column = 0; column < MapWidth; ++column)
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

    private void DrawEnemy()
    {
        drawBuffer[enemy.Position.Line, enemy.Position.Column] = 'E';
    }

    private void Flush()
    {
        for(int line = 0; line < MapHeigth; ++line)
        {
            for(int column = 0; column < MapWidth; ++column)
            {
                if(line < Console.BufferHeight && column < Console.BufferWidth - 1)
                    Console.Write(drawBuffer[line, column]);
            }
            Console.WriteLine();
        }
    }

    private bool gameStopped;
    private static Game? instance;

    private readonly char[,] drawBuffer;
    private readonly Room room;
    private readonly Player player;
    private readonly Enemy enemy;
}
