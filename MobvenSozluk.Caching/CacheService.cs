using System.Text.Json;
using MobvenSozluk.Caching.Configurations;
using MobvenSozluk.Repository.Cache;
using StackExchange.Redis;

namespace MobvenSozluk.Caching;

public class CacheService<T> : ICacheService<T>
{
    private readonly IDatabase _cache;


    public CacheService(RedisConfiguration config)
    {
        var connection = ConnectionMultiplexer.Connect(config.ConnectionString);
        _cache = connection.GetDatabase();
    }
    
    public IEnumerable<T> Get(string key)
    {
        string cachedValue = _cache.StringGet(key);
        if (!string.IsNullOrEmpty(cachedValue))
        {
            return JsonSerializer.Deserialize<IEnumerable<T>>(cachedValue);
        }

        return default;
    }

    public bool Set(string key, IEnumerable<T> value)
    {
        var serializedValue = JsonSerializer.Serialize(value);
        return _cache.StringSet(key, serializedValue);
    }

    public object Remove(string key)
    {
        var keyExists = _cache.KeyExists(key);
        return keyExists && _cache.KeyDelete(key);
    }

    public bool Exists(string key)
    {
        return _cache.KeyExists(key);
    }

}