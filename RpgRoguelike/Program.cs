using RpgRoguelike;
using RpgRoguelike.Services.Building.Rooms;

Game.Instance.Init(
    new RandomRoomBuilder()
            .SetSize(40, 15)
            .SetRandomSeed((int)DateTime.Now.Ticks)
);
Game.Instance.Run();
