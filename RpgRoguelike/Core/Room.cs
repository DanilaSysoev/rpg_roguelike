using RpgRoguelike.Exceptions;
using RpgRoguelike.Gameplay;

namespace RpgRoguelike.Core;

public class Room
{
    public IEnumerable<Cell> Cells => cells.Values;

    public Room(int width, int height)
    {
        if (width < 3 || height < 3)
            throw new RoomSizeException(width, height);
        
        for(int line = 0; line < height; ++line)
        {
            for(int column = 0; column < width; ++column)
            {
                if(line == 0 || column == 0 || line == height - 1 || column == width - 1)
                    AddCell(new Cell(new Position(line, column), false));
                else
                    AddCell(CreateInsideCell(line, column));
            }
        }
    }

    private static Cell CreateInsideCell(int line, int column)
    {
        Cell result = new Cell(new Position(line, column), true);
        Random random = new Random((int)DateTime.Now.Ticks);
        while(random.Next(100) < 5)
            result.AddReward(CreateReward());
            
        return result;
    }

    private static IReward CreateReward()
    {
        Random random = new Random((int)DateTime.Now.Ticks);
        switch(random.Next(2))
        {
            case 0:
                return new HealthReward(random.Next(5, 10));
            default:
                return new GoldReward(random.Next(10, 20));
        }
    }

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
