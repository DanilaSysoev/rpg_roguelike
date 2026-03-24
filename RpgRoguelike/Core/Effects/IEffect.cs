namespace RpgRoguelike.Core.Effects;

public interface IEffect
{
    bool IsActive { get; }

    void Apply(Entity target);
}
