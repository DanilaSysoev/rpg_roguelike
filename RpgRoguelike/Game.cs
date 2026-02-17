namespace RpgRoguelike;

public class Game
{
    public void Init()
    {
        gameStopped = false;
        Console.Clear();
        Console.WriteLine("Game is running... Press Escape to stop.");
    }

    public void Run()
    {
        while (!GemeIsEnded())
        {
            HandleInput();
            Update();
            Render();
        }
    }

    private bool GemeIsEnded()
    {
        return gameStopped;
    }

    private void HandleInput()
    {
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape)
            gameStopped = true;
    }

    private void Update()
    {
        // Now empty because we don't have any game logic yet.
    }

    private static void Render()
    {
        Console.Clear();
        Console.WriteLine("Game is running... Press Escape to end game.");
    }
    

    private bool gameStopped = false;
}
