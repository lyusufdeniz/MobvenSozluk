namespace MobvenSozluk.Repository.Cache;

public interface ICacheService<T>
{
    T Get<T>(string key);
    List<T> GetAll(string key);
    bool Set(string key, IEnumerable<T> value, DateTimeOffset expirationTime);
    object Remove(string key);
    bool Exists(string key);
}