namespace RpgRoguelike.Core.GameStates;

public interface IGameState
{
    void Render();
    void HandleInput();
    void Update();
}
