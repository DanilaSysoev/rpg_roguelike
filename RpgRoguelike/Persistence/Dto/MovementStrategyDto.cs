namespace RpgRoguelike.Persistence.Dto;


class MovementStrategyDto : DtoBase
{
    public string Type { get; set; } = "";
    public string Direction { get; set; } = "";
    public int LeftBorder { get; set; }
    public int RightBorder { get; set; }
    public int TopBorder { get; set; }
    public int BottomBorder { get; set; }
    public string ClockwiseDirection { get; set; } = "";
    public int PosTopLeftLine { get; set; }
    public int PosTopLeftCol { get; set; }
    public int PosBotRightLine { get; set; }
    public int PosBotRightCol { get; set; }
}
