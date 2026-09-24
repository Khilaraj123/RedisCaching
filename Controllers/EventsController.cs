using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisCaching.Models;
using System.Text.Json;

namespace RedisCaching.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        public EventsController(IDistributedCache cache)
        {
            _cache = cache;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEvent(int id)
        {
            string cacheKey = $"event:{id}";

            var cachedData = await _cache.GetStringAsync(cacheKey);

            if(cachedData is not null)
            {
                var cachedEvent = JsonSerializer.Deserialize<EventDto>(cachedData);
                return Ok(cachedEvent);
            }
            var eventData = new EventDto
            {
                Id = id,
                Name = "Nepal Music Fest",
                Price = 599
            };

            var serializedData = JsonSerializer.Serialize(eventData);

            await _cache.SetStringAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return Ok(eventData);
        }
    }
}
