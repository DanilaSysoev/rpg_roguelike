using RpgRoguelike.Core;

namespace RpgRoguelike.Persistence;

public interface IGameRepository
{
    void SavePlayer(Player player);
    Player LoadPlayer();

    void SaveRoom(Room room);
    Room LoadRoom();

    bool CanBeLoaded();
    void LoadFinish();
}
