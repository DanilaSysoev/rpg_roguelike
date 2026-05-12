namespace RpgRoguelike.Persistence.Dto;


class EffectDto : DtoBase
{
    public string Type { get; set; } = "";
    public int Time { get; set; }
    public int BleedingValue { get; set; }
    public int BurningPower { get; set; }
}
