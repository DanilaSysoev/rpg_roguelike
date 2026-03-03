using RpgRoguelike.Core;

namespace RpgRoguelike;

public class Game
{
    public int MapWidth { get; private set; }
    public int MapHeigth { get; private set; }


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
                if (room.CanMoveTo(player.Position.UpNeighbor()))
                    player.Move(Direction.Up);
                break;
            case ConsoleKey.RightArrow:
                if(room.CanMoveTo(player.Position.RightNeighbor()))
                    player.Move(Direction.Right);
                break;
            case ConsoleKey.DownArrow:
                if (room.CanMoveTo(player.Position.DownNeighbor()))
                    player.Move(Direction.Down);
                break;
            case ConsoleKey.LeftArrow:
                if (room.CanMoveTo(player.Position.LeftNeighbor()))
                    player.Move(Direction.Left);
                break;
        }
    }

    private void Update()
    {
        Cell curr = room.GetCell(player.Position);
        foreach(var reward in curr.Rewards)
            reward.Get(player);

        curr.RemoveAllRewards();
    }

    private void Render()
    {
        Console.Clear();
        ClearDrawBuffer();
        DrawRoom();
        DrawPlayer();
        Flush();
        DrawStatus();
    }

    private void DrawStatus()
    {
        Console.WriteLine($"Health: {player.Health} / {player.MaxHealth}");
        Console.WriteLine($"Gold: {player.Gold}");
    }
    
    private Game()
    {
        MapHeigth = 15;
        MapWidth = 40;

        drawBuffer = new char[MapHeigth, MapWidth];
        room = new Room(MapWidth, MapHeigth);
        player = new Player(100, 50, new Position(5, 5));

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
}
