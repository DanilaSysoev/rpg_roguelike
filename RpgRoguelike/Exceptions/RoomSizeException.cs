namespace RpgRoguelike.Exceptions;

public class RoomSizeException : Exception
{
    public RoomSizeException(int width, int height)
        : base($"Room size must be at least 3x3, but was {width}x{height}")
    {
    }
}
