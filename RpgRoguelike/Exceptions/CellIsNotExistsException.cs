using RpgRoguelike.Core;

namespace RpgRoguelike.Exceptions;

public class CellIsNotExistsException : Exception
{
    public CellIsNotExistsException(Position position)
        : base($"Cell with position {position} does not exists")
    {
    }
}
