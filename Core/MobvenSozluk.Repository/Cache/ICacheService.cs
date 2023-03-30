namespace MobvenSozluk.Repository.Cache;

public interface ICacheService<T>
{
    T Get<T>(string key);
    bool Set<T>(string key, T value, DateTimeOffset expirationTime);
    bool Set<T>(string key, IEnumerable<T> value, DateTimeOffset expirationTime);
    bool Remove(string key);
    bool Exists(string key);
}