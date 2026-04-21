using System.Runtime.CompilerServices;
using RpgRoguelike.Exceptions;

[assembly: InternalsVisibleTo("RpgRoguelike.Tests")]
namespace RpgRoguelike.Core;

public class Room
{
    public IEnumerable<Cell> Cells => cells.Values;
    public IEnumerable<Enemy> Enemies => enemies;

    public int MinLine { get; private set; }
    public int MaxLine { get; private set; }
    public int MinColumn { get; private set; }
    public int MaxColumn { get; private set; }

    public int Width => MaxColumn - MinColumn + 1;
    public int Height => MaxLine - MinLine + 1;

    internal Room()
    {}

    public void AddCell(Cell cell)
    {
        if(!cells.ContainsKey(cell.Position))
            cells.Add(cell.Position, cell);
        else
            cells[cell.Position] = cell;
        
        UpdateMinMax(cell.Position);
    }

    public Cell GetCell(Position position)
    {
        if (!cells.ContainsKey(position))
            throw new CellIsNotExistsException(position);
        return cells[position];
    }

    public void AddEnemy(Enemy enemy)
    {
        if (!cells.ContainsKey(enemy.Position))
            throw new CellIsNotExistsException(enemy.Position);
        if (!cells[enemy.Position].IsPassable)
            throw new CellIsNotPassableException(enemy.Position);
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public void Update()
    {
        enemies.RemoveAll(e => !e.IsAlive);
        foreach (var enemy in enemies)
            enemy.Update();
    }

    public bool CanMoveTo(Position position)
    {
        return cells.ContainsKey(position) && 
               cells[position].IsPassable &&
               enemies.All(e => e.Position != position) &&
               Game.Instance.Player.Position != position;
    }

    private void UpdateMinMax(Position position)
    {
        if(position.Line < MinLine) MinLine = position.Line; 
        if(position.Line > MaxLine) MaxLine = position.Line;
        if(position.Column < MinColumn) MinColumn = position.Column;
        if(position.Column > MaxColumn) MaxColumn = position.Column;
    }

    public Enemy? GetEnemyAt(Position nextPosition)
    {
        return enemies.FirstOrDefault(e => e.Position == nextPosition);
    }

    private readonly Dictionary<Position, Cell> cells = new();
    private readonly List<Enemy> enemies = new();
}
