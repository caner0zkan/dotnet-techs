using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase {
        readonly IMemoryCache _memoryCache;
        readonly IDistributedCache _distributedCache;

        public ValueController(IMemoryCache memoryCache, IDistributedCache distributedCache) {
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public string GetName() {
            return _memoryCache.Get<string>("name");
        }

        [HttpPost]
        public void SetName(string name) {
            _memoryCache.Set("name", name);
        }

        [HttpGet("getDate")]
        public DateTime GetDate() {
            return _memoryCache.Get<DateTime>("date");
        }

        [HttpPost("setDate")]
        public void SetDate(string name) {
            _memoryCache.Set<DateTime>("date", DateTime.UtcNow, options: new() {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpPost("setAdress")]
        public async Task<IActionResult> SetAdress(string adress, string zipcode) {
            await _distributedCache.SetStringAsync("adress", adress, options: new() {
                AbsoluteExpiration = DateTime.UtcNow.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
            await _distributedCache.SetAsync("zipcode", Encoding.UTF8.GetBytes(zipcode), options: new() {
                AbsoluteExpiration = DateTime.UtcNow.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });

            return Ok();
        }

        [HttpGet("getAdress")]
        public async Task<IActionResult> GetAdress() {
            var adress = await _distributedCache.GetStringAsync("adress");
            var zipcode = await _distributedCache.GetAsync("zipcode");
            Encoding.UTF8.GetString(zipcode);

            return Ok(new {
                adress, 
                zipcode
            });
        }
    }
}
