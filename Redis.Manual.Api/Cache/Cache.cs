using System.Threading.Tasks;

namespace Redis.Manual.Api.Cache
{
    public interface Cache
    {
         
         Task<T> Get<T>(string key) where T : class;

         Task Set(string key, object value, int ttl);

         Task Delete(string key);
    }
}