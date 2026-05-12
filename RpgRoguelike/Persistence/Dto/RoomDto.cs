namespace RpgRoguelike.Persistence.Dto;


class RoomDto : DtoBase
{
    public List<int> CellIds { get; set; } = new();
    public List<int> EnemyIds { get; set; } = new();
}
