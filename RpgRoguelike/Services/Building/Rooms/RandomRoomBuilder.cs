using RpgRoguelike.Core;
using RpgRoguelike.Core.EnemyStrategies;
using RpgRoguelike.Exceptions;
using RpgRoguelike.Gameplay;
using RpgRoguelike.Services.Base;
using RpgRoguelike.Services.Building.Entities;
using RpgRoguelike.Services.Building.Rewards;
using RpgRoguelike.Services.Building.Weapons;

namespace RpgRoguelike.Services.Building.Rooms;

public class RandomRoomBuilder : IRoomBuilder
{
    public Room Build()
    {
        Room room = new();

        if (width < 3 || height < 3)
            throw new RoomSizeException(width, height);
        
        for(int line = 0; line < height; ++line)
        {
            for(int column = 0; column < width; ++column)
            {
                if(line == 0 || column == 0 || line == height - 1 || column == width - 1)
                    room.AddCell(new Cell(new Position(line, column), false));
                else
                    room.AddCell(CreateInsideCell(line, column));
            }
        }

        BuildEnemies(room);

        return room;
    }

    public RandomRoomBuilder SetWidth(int width)
    {
        this.width = width;
        return this;
    }
    public RandomRoomBuilder SetHeight(int height)
    {
        this.height = height;
        return this;
    }
    public RandomRoomBuilder SetSize(int width, int height)
    {
        this.width = width;
        this.height = height;
        return this;
    }
    public RandomRoomBuilder SetRandomSeed(int seed)
    {
        random = new Random(seed);
        return this;
    }
    public RandomRoomBuilder SetRewardChance(double chance)
    {
        rewardChance = chance;
        return this;
    }
    public RandomRoomBuilder SetHealthRewardValueRange(
        int minValue, int maxValue
    )
    {
        healthRewardMinValue = minValue;
        healthRewardMaxValue = maxValue;
        return this;
    }
    public RandomRoomBuilder SetGoldRewardValueRange(
        int minValue, int maxValue
    )
    {
        goldRewardMinValue = minValue;
        goldRewardMaxValue = maxValue;
        return this;
    }

    private Cell CreateInsideCell(int line, int column)
    {
        Cell result = new Cell(new Position(line, column), true);
        while(random.NextDouble() < rewardChance)
            result.AddReward(CreateReward());
            
        return result;
    }

    private IReward CreateReward()
    {
        List<IRewardCreator> rewardCreators = BuildRewardCreators();        
        return rewardCreators[random.Next(rewardCreators.Count)].Create();
    }

    private List<IRewardCreator> BuildRewardCreators()
    {
        return
        [
            new RandomHealthRewardCreator(
                healthRewardMinValue, healthRewardMaxValue, random
            ),
            new RandomGoldRewardCreator(
                goldRewardMinValue, goldRewardMaxValue, random
            ),
        ];
    }

    private void BuildEnemies(Room room)
    {
        var count = random.Next(MinEnemieCount, MaxEnemieCount + 1);
        for(int i = 0; i < count; ++i)
            BuildEnemy(room);
    }

    private void BuildEnemy(Room room)
    {
        room.AddEnemy(
            enemyBuilders[random.Next(enemyBuilders.Count)](room, random)
        );
    }

    private Random random = new Random((int)DateTime.Now.Ticks);
    private int width;
    private int height;
    private double rewardChance = DefaultRewardChance;
    private int healthRewardMinValue = DefaultHealthRewardMinValue;
    private int healthRewardMaxValue = DefaultHealthRewardMaxValue;
    private int goldRewardMinValue = DefaultGoldRewardMinValue;
    private int goldRewardMaxValue = DefaultGoldRewardMaxValue;

    private const double DefaultRewardChance = 0.05;
    private const int DefaultHealthRewardMinValue = 5;
    private const int DefaultHealthRewardMaxValue = 10;
    private const int DefaultGoldRewardMinValue = 10;
    private const int DefaultGoldRewardMaxValue = 20;
    private const int MinEnemieCount = 1;
    private const int MaxEnemieCount = 5;

    private readonly List<Func<Room, Random, Enemy>> enemyBuilders = 
    [
        (room, random) => BuildHorizontalPatrolEnemy(room, random),
        (room, random) => BuildVerticalPatrolEnemy(room, random),
        (room, random) => BuildRectangularPatrolEnemy(room, random),
    ];

    private static Enemy BuildRectangularPatrolEnemy(Room room, Random random)
    {
        int rectWidth = random.Next(Game.Instance.MapWidth / 4, Game.Instance.MapWidth / 2);
        int rectHeight = random.Next(Game.Instance.MapHeight / 4, Game.Instance.MapHeight / 2);
        while(true)
        {
            Position topLeft = new Position(
                random.Next(1, Game.Instance.MapHeight - rectHeight - 2),
                random.Next(1, Game.Instance.MapWidth - rectWidth - 2)
            );
            if (room.GetCell(topLeft).IsPassable)                
                return new EnemyBuilder()
                           .SetMovementStrategy(
                                new RectanglePatrol(
                                    topLeft,
                                    new Position(
                                        topLeft.Line + rectHeight,
                                        topLeft.Column + rectWidth
                                    )
                                )
                            )
                           .SetMaxHealth(50)
                           .SetHealth(50)
                           .SetPosition(topLeft)
                           .SetWeapon(
                                new DaggerBuilder()
                                    .SetName("Old rusty dagger")
                                    .SetDamage(5)
                                    .Build()
                                )
                           .SetName("Skeleton")
                           .Build();
        }
    }

    private static Enemy BuildVerticalPatrolEnemy(Room room, Random random)
    {
        int col = random.Next(1, Game.Instance.MapWidth - 2);
        while(true)
        {
            Position pos = new Position(
                random.Next(1, Game.Instance.MapHeight - 2),
                col
            );
            if (room.GetCell(pos).IsPassable)                
                return new EnemyBuilder()
                           .SetMovementStrategy(new VerticalLinePatrol())
                           .SetMaxHealth(50)
                           .SetHealth(50)
                           .SetPosition(pos)
                           .SetWeapon(
                                new SwordBuilder()
                                    .SetName("Rusty sword")
                                    .SetDamage(5)
                                    .Build())
                           .SetName("Goblin")
                           .Build();
        }
    }

    private static Enemy BuildHorizontalPatrolEnemy(Room room, Random random)
    {
        int line = random.Next(1, Game.Instance.MapHeight - 2);
        while(true)
        {
            Position pos = new Position(
                line,
                random.Next(1, Game.Instance.MapWidth - 2)
            );
            if (room.GetCell(pos).IsPassable)                
                return new EnemyBuilder()
                           .SetMovementStrategy(new HorizontalLinePatrol())
                           .SetMaxHealth(50)
                           .SetHealth(50)
                           .SetPosition(pos)
                           .SetWeapon(
                                new HammerBuilder().SetName("Skull Crusher")
                                                   .SetDamage(5)
                                                   .Build())
                           .SetName("Orc")
                           .Build();
        }
    }
}
