using RpgRoguelike.Exceptions;

namespace RpgRoguelike.Core;

public class Room
{
    public void AddCell(Cell cell)
    {
        if(!cells.ContainsKey(cell.Position))
            cells.Add(cell.Position, cell);
        else
            cells[cell.Position] = cell;
    }

    public Cell GetCell(Position position)
    {
        if (!cells.ContainsKey(position))
            throw new CellNotExistsException(position);
        return cells[position];
    }

    public bool CanMoveTo(Position position)
    {
        return cells.ContainsKey(position) && 
               cells[position].IsPassable;
    }

    private readonly Dictionary<Position, Cell> cells = new();
}
