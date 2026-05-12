using RpgRoguelike.Persistence.Dto;
using System.Text.Json;

namespace RpgRoguelike.Persistence.Json;


class JsonRepository<T> : IRepository<T> where T : DtoBase
{
    public JsonRepository() { }

    public JsonRepository(string filename)
    {
        this.filename = filename;
    }

    public void Add(T dto)
    {
        ++maxId;
        dto.Id = maxId;
        data.Add(dto);
    }

    public T Get(int id)
    {
        var value = data.Find(dto => dto.Id == id);
        if(value is null)
            throw new ArgumentException("Invalid DTO Id");
        return value;
    }

    public void Load()
    {
        if(File.Exists(filename))
            data = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(filename))!;
        if(data.Count > 0)
            maxId = data.Max(dto => dto.Id);
    }

    public void Remove(T dto)
    {
        data.Remove(dto);
    }

    public void Save()
    {
        File.WriteAllText(filename, JsonSerializer.Serialize(data));
    }

    public void Clear()
    {
        maxId = 0;
        data.Clear();
    }

    public IEnumerable<T> GetAll() => data;

    private readonly string filename = DEFAULT_FILENAME;
    private static readonly string DEFAULT_FILENAME = typeof(T).Name + ".json";
    private List<T> data = new();
    private int maxId = 0;
}
