using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KocUniversityCourseManagement.Presentation.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class RedisTestController : ControllerBase
        {
            private readonly IDatabase _redisDatabase;

            public RedisTestController(IConnectionMultiplexer redisMultiplexer)
            {
                _redisDatabase = redisMultiplexer.GetDatabase();
            }

            [HttpGet("set/{key}/{value}")]
            public IActionResult SetKeyValue(string key, string value)
            {
                _redisDatabase.StringSet(key, value);
                return Ok($"Key: {key} with Value: {value} has been set in Redis.");
            }

            [HttpGet("get/{key}")]
            public IActionResult GetKeyValue(string key)
            {
                var value = _redisDatabase.StringGet(key);
                if (value.IsNullOrEmpty)
                {
                    return NotFound("Key not found in Redis.");
                }

                return Ok($"Value for Key: {key} is {value}");
            }
    }
}

