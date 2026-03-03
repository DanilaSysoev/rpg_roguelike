namespace RpgRoguelike.Core;

public class Player
{
    public int Golg { get; set; }
    public int Health
    { 
        get
        {
            return health;
        }
        set
        {
            health = Math.Min(value, MaxHealth);
        }
    }
    public int MaxHealth { get; private set; }

    public Position Position { get; set; }

    public Player(int maxHealth)
    {
        if(maxHealth < 1)
            throw new ArgumentException("Max health must be greater than 0");

        MaxHealth = maxHealth;
        Health = maxHealth;
        Position = new Position(0, 0);
    }

    public Player(int maxHealth, Position position)
    {
        if(maxHealth < 1)
            throw new ArgumentException("Max health must be greater than 0");

        MaxHealth = maxHealth;
        Health = maxHealth;
        Position = position;
    }

    public void Move(Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                Position = Position.UpNeighbor();
                break;
            case Direction.Right:
                Position = Position.RightNeighbor();
                break;
            case Direction.Down:
                Position = Position.DownNeighbor();
                break;
            case Direction.Left:
                Position = Position.LeftNeighbor();
                break;
        }
    }

    private int health;
}
