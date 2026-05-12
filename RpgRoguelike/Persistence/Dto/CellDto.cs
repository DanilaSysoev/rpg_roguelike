namespace RpgRoguelike.Persistence.Dto;


class CellDto : DtoBase
{
    public bool IsPassable { get; set; }
    public int PosLine { get; set; }
    public int PosCol { get; set; }
    public List<int> RewardIds { get; set; } = new();
}
