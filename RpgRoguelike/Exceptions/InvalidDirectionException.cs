using RpgRoguelike.Core;

namespace RpgRoguelike.Exceptions;

public class InvalidDirectionException : Exception
{
    public InvalidDirectionException(Direction direction)
        : base($"Invalid direction: {direction}")
    {}
}
