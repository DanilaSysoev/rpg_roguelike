using RpgRoguelike.Core;
using RpgRoguelike.Core.Control;
using RpgRoguelike.Services.Building.Entities;
using RpgRoguelike.Tests.Services;

namespace RpgRoguelike.Tests;

public class PlayerTests
{
    private Player player;
    private MotionCommand upMotion;
    private MotionCommand downMotion;
    private MotionCommand leftMotion;
    private MotionCommand rightMotion;


    [SetUp]
    public void Setup()
    {
        Game.Instance.Init(new TestRoomBuilder());

        player = new PlayerBuilder().SetMaxHealth(100)
                                    .SetHealth(100)
                                    .SetPosition(new Position(0, 0))
                                    .SetName("Player")
                                    .Build();
        upMotion = new MotionCommand { Direction = Direction.Up };
        upMotion.OnExecute += player.MotionCommandHandler;
        downMotion = new MotionCommand { Direction = Direction.Down };
        downMotion.OnExecute += player.MotionCommandHandler;
        leftMotion = new MotionCommand { Direction = Direction.Left };
        leftMotion.OnExecute += player.MotionCommandHandler;
        rightMotion = new MotionCommand { Direction = Direction.Right };
        rightMotion.OnExecute += player.MotionCommandHandler;
    }

    [TearDown]
    public void TearDown() => Game.Cleanup();

    [Test]
    public void Move_MoveUp_PositionLineDecreases()
    {
        upMotion.Execute();
        player.Update();
        Assert.That(player.Position, Is.EqualTo(new Position(-1, 0)));
    }
    [Test]
    public void Move_MoveDown_PositionLineIncreases()
    {
        downMotion.Execute();
        player.Update();
        Assert.That(player.Position, Is.EqualTo(new Position(1, 0)));
    }
    [Test]
    public void Move_MoveLeft_PositionColumnDecreases()
    {
        leftMotion.Execute();
        player.Update();
        Assert.That(player.Position, Is.EqualTo(new Position(0, -1)));
    }
    [Test]
    public void Move_MoveRight_PositionColumnIncreases()
    {
        rightMotion.Execute();
        player.Update();
        Assert.That(player.Position, Is.EqualTo(new Position(0, 1)));
    }
}
