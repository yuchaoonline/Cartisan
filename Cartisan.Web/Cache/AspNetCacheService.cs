using System.Web;
using Cartisan.Cache;

namespace Cartisan.Web.Cache {
    public class AspNetCacheService: ICacheService {
        private readonly System.Web.Caching.Cache _cache;
        public AspNetCacheService() {
            if (HttpContext.Current != null) {
                _cache = HttpContext.Current.Cache;
            }
        }

        public object Get(string key) {
            return _cache[key];
        }

        public void Set(string key, object data) {
            _cache[key] = data;
        }

        public object this[string key] {
            get { return _cache[key]; }
            set { _cache[key] = value; }
        }
    }
}