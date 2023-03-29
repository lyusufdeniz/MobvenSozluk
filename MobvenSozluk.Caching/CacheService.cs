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
        var connection = ConnectionMultiplexer.Connect(config.RedisConnection);
        _cache = connection.GetDatabase();
    }
    
    public T Get<T>(string key)
    {
        string cachedValue = _cache.StringGet(key);
        if (!string.IsNullOrEmpty(cachedValue))
        {
            return JsonSerializer.Deserialize<T>(cachedValue);
        }
        return default;
    }
    public bool Set<T>(string key, T value, DateTimeOffset expirationTime)
    {
        var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
        var serializedValue = JsonSerializer.Serialize(value);
        return _cache.StringSet(key, serializedValue);
    }
    public bool Set<T>(string key, IEnumerable<T> value, DateTimeOffset expirationTime)
    {
        return Set(key, value.ToList(), expirationTime);
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