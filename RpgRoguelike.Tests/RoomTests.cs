using System;
using RpgRoguelike.Core;
using RpgRoguelike.Exceptions;
using RpgRoguelike.Tests.Services;

namespace RpgRoguelike.Tests;

public class RoomTests
{
    Room room;

    [SetUp]
    public void Setup()
    {
        room = new TestRoomBuilder().Build();
    }

    [Test]
    public void GetCell_NonExistingPosition_ThrowsException()
    {
        Assert.Throws<CellNotExistsException>(
            () => room.GetCell(new Position(100, 100))
        );
    }

    [Test]
    public void GetCell_NonExistingPosition_ErrorMsgConatainPosition()
    {
        var pos = new Position(100, 100);

        var exc = Assert.Throws<CellNotExistsException>(
            () => room.GetCell(pos)
        );

        exc.Message.Contains(pos.ToString());
    }
}
