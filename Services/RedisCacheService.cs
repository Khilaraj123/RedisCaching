using Microsoft.Extensions.Caching.Distributed;
using RedisCaching.Interfaces;
using System.Text.Json;

namespace RedisCaching.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
        {
            var json = await _cache.GetStringAsync(Key)
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
