namespace Cartisan.Cache {
    public interface ICacheService {
        object Get(string key);
        void Set(string key, object data);
        object this[string key] { get; set; }
    }
}