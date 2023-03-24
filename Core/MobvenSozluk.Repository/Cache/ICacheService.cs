namespace MobvenSozluk.Repository.Cache;

public interface ICacheService<T>
{
    IEnumerable<T> Get(string key);
    bool Set(string key, IEnumerable<T> value);
    object Remove(string key);
    bool Exists(string key);
}