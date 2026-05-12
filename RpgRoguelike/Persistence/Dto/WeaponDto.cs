namespace RpgRoguelike.Persistence.Dto;


class WeaponDto : DtoBase
{
    public int ChildId { get; set; }

    public string Name { get; set; } = "";
    public int Damage { get; set; }
    public string Type { get; set; } = "";
    public int BleedingValue { get; internal set; }
    public int BleedingTime { get; internal set; }
    public int StunChance { get; internal set; }
    public int StunTime { get; internal set; }
    public int FireDamage { get; internal set; }
    public int BurningChance { get; internal set; }
    public int BurningTime { get; internal set; }
    public int BurningPower { get; internal set; }
}
