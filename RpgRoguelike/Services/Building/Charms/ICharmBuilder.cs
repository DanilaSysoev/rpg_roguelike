using RpgRoguelike.Core.Weapons.Charms;

namespace RpgRoguelike.Services.Building.Charms;

public interface ICharmBuilder<out T> where T : CharmBase, new()
{
    T Build();
}
