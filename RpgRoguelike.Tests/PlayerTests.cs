using RpgRoguelike.Core;

namespace RpgRoguelike.Tests;

public class PlayerTests
{
    private Player player;

    [SetUp]
    public void Setup()
    {
        player = new Player(100, new Position(0, 0));
    }

    [Test]
    public void Move_MoveUp_PositionLineDecreases()
    {
        player.Move(Direction.Up);
        Assert.That(player.Position, Is.EqualTo(new Position(-1, 0)));
    }
    [Test]
    public void Move_MoveDown_PositionLineIncreases()
    {
        player.Move(Direction.Down);
        Assert.That(player.Position, Is.EqualTo(new Position(1, 0)));
    }
    [Test]
    public void Move_MoveLeft_PositionColumnDecreases()
    {
        player.Move(Direction.Left);
        Assert.That(player.Position, Is.EqualTo(new Position(0, -1)));
    }
    [Test]
    public void Move_MoveRight_PositionColumnIncreases()
    {
        player.Move(Direction.Right);
        Assert.That(player.Position, Is.EqualTo(new Position(0, 1)));
    }
}
