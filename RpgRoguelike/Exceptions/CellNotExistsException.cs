using RpgRoguelike.Core;

namespace RpgRoguelike.Exceptions;

public class CellNotExistsException : Exception
{
    public CellNotExistsException(Position position)
        : base($"Cell with position {position} does not exists")
    {
    }
}
