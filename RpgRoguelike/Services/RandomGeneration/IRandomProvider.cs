namespace RpgRoguelike.Services.RandomGeneration;

public interface IRandomProvider
{
    bool CheckChance(int chance);
}
