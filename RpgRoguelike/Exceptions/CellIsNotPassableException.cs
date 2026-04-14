using RpgRoguelike.Core;

namespace RpgRoguelike.Exceptions;

public class CellIsNotPassableException : Exception
{
    public CellIsNotPassableException(Position position)
        : base($"Cell with position {position} is not passable")
    {
    }
}
