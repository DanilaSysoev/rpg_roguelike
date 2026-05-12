namespace RpgRoguelike.Persistence.Dto;


class EntityDto : DtoBase
{
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int PosLine { get; set; }
    public int PosCol { get; set; }
    public bool IsStunned { get; set; }
    public int WeaponId { get; set; }
    public List<int> EffectIds { get; set; } = new();
}
