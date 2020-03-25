using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Redis.Manual.Api.Cache
{
    public class CacheRedis : Cache
    {

        private readonly IConfiguration _configuration;
        private readonly IDatabase _database;
        private readonly ConnectionMultiplexer _connection;

        public CacheRedis(IConfiguration configuration)
        {
            _configuration = configuration;

            if(_connection is null  || !_connection.IsConnected)
                _connection = ConnectionMultiplexer.Connect(_configuration["CacheConfig:Server"]);

            _database = _connection.GetDatabase();

        }

        public async Task Delete(string key)
        {
            await _database.KeyDeleteAsync(key);
        }

        public async Task<T> Get<T>(string key) where T : class
        {
            var result = await _database.StringGetAsync(key);

            return JsonConvert.DeserializeObject<T>(result);
        }

        public async Task Set(string key, object value, int ttl = -1)
        {
            if(ttl == -1)
                await _database.StringSetAsync(key, JsonConvert.SerializeObject(value));
            else
                await _database.StringSetAsync(key, JsonConvert.SerializeObject(value), TimeSpan.FromMinutes(ttl));
        
        }
    }
}