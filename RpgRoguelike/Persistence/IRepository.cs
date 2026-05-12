namespace RpgRoguelike.Persistence.Dto;


interface IRepository<T> where T : DtoBase
{
    void Load();
    void Save();

    T Get(int id);
    void Add(T dto);
    void Remove(T dto);
    IEnumerable<T> GetAll();

    void Clear();
}
