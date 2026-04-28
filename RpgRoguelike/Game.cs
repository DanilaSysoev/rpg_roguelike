using RpgRoguelike.Core;
using RpgRoguelike.Core.Drawing;
using RpgRoguelike.Core.GameStates;
using RpgRoguelike.Services.Base;
using RpgRoguelike.Services.Building.Entities;
using RpgRoguelike.Services.Building.Weapons;

namespace RpgRoguelike;

public class Game
{
    public Player Player => player;
    public Room Room => room;
    public IGameState State { get; internal set; } = null!;

    public static Game Instance
    {
        get
        {
            if (instance == null)
                instance = new Game();
            return instance;
        }
    }

    public void Init(IRoomBuilder roomBuilder)
    {
        room = roomBuilder.Build();

        
        State = new GameplayState(new DynamicDrawBuffer());
        gameStopped = false;
    }

    public static void Cleanup() { instance = null; }

    public void Run()
    {
        State.Render();
        while (!GemeIsEnded())
        {
            State.HandleInput();
            State.Update();
            State.Render();
        }
    }

    public void Stop()
    {
        gameStopped = true;
    }

    private bool GemeIsEnded()
    {
        return gameStopped || !player.IsAlive;
    }
    
    private Game()
    {
        var hammer = new HammerBuilder().SetStunChance(100)
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

    private bool gameStopped;
    private static Game? instance;

    private Room room = null!;
    private readonly Player player;
}
