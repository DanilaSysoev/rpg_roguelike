namespace RpgRoguelike.Core.Drawing;

public interface IDrawBuffer
{
    void Clear();
    void Flush();
    void Add(int x, int y, char symbol);
    void Add(int x, int y, string line);
}
