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

    public Player(int maxHealth)
    {
        if(maxHealth < 1)
            throw new ArgumentException("Max health must be greater than 0");

        MaxHealth = maxHealth;
        Health = maxHealth;
    }

    private int health;
}
