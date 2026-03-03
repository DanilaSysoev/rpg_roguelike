namespace RpgRoguelike.Core;

public struct Position
{
    public int Line { get; private set; }
    public int Column { get; private set; }

    public Position(int line, int column)
    {
        Line = line;
        Column = column;
    }

    public Position UpNeighbor()
    {
        return new Position(Line - 1, Column);
    }

    public Position DownNeighbor()
    {
        return new Position(Line + 1, Column);
    }

    public Position LeftNeighbor()
    {
        return new Position(Line, Column - 1);
    }

    public Position RightNeighbor()
    {
        return new Position(Line, Column + 1);
    }

    public override string ToString()
    {
        return $"({Line}, {Column})";
    }
}
