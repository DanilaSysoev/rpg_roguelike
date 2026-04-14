using RpgRoguelike.Core;

namespace RpgRoguelike.Services.Base;

public interface IEntityBuilder<out T> where T : Entity, new()
{
    T Build();
}
