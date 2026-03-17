using System;
using RpgRoguelike.Core;
using RpgRoguelike.Services.Base;

namespace RpgRoguelike.Tests.Services;

public class TestRoomBuilder : IRoomBuilder
{
    public Room Build()
    {
        var room = new Room();

        room.AddCell(new Cell(new Position(0, 0), true));
        room.AddCell(new Cell(new Position(0, 1), true));
        room.AddCell(new Cell(new Position(1, 0), true));
        room.AddCell(new Cell(new Position(1, 1), false));
        room.AddCell(new Cell(new Position(0, -1), true));
        room.AddCell(new Cell(new Position(-1, 0), true));
        room.AddCell(new Cell(new Position(-1, -1), false));
        room.AddCell(new Cell(new Position(-1, 1), false));
        room.AddCell(new Cell(new Position(1, -1), false));

        return room;
    }
}
