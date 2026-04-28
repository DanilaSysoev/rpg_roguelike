namespace RpgRoguelike.Core.Drawing;

public class DynamicDrawBuffer : IDrawBuffer
{
    public void Add(int x, int y, char symbol)
    {
        VerticallyExpandBuffer(y + 1);
        HorizontallyExpandBuffer(y, x + 1);
        buffer[y][x] = symbol;
    }

    public void Add(int x, int y, string line)
    {
        VerticallyExpandBuffer(y + 1);
        HorizontallyExpandBuffer(y, x + line.Length);
        for(int i = 0; i < line.Length; i++)
            buffer[y][x + i] = line[i];
    }

    public void Clear()
    {
        buffer.Clear();
    }

    public void Flush()
    {
        for(int line = 0; line < buffer.Count; line++)
        {
            for(int col = 0; col < buffer[line].Count; col++)
            {
                if (line < Console.BufferHeight && col < Console.BufferWidth)
                    Console.Write(buffer[line][col]);
            }
            Console.WriteLine();
        }
    }

    
    private readonly List<List<char>> buffer = new();

    private void VerticallyExpandBuffer(int newHeight)
    {
        while(buffer.Count < newHeight)
            buffer.Add(new List<char>());
    }

    private void HorizontallyExpandBuffer(int line, int newWidth)
    {
        while(buffer[line].Count < newWidth)
            buffer[line].Add(' ');
    }
}