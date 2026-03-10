using RpgRoguelike.Core;
using RpgRoguelike.Exceptions;
using RpgRoguelike.Gameplay;
using RpgRoguelike.Services.Base;

namespace RpgRoguelike.Services;

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
}
