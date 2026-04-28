using RpgRoguelike.Core.Drawing;

namespace RpgRoguelike.Core.GameStates;

public abstract class GameState : IGameState
{
    protected IDrawBuffer DrawBuffer => drawBuffer;

    protected GameState(IDrawBuffer drawBuffer)
    {
        this.drawBuffer = drawBuffer;
    }

    public void Render()
    {
        Console.Clear();
        drawBuffer.Clear();
        PrepareDrawBuffer();
        drawBuffer.Flush();
    }

    protected abstract void PrepareDrawBuffer();
    public abstract void HandleInput();
    public abstract void Update();

    private readonly IDrawBuffer drawBuffer;
}