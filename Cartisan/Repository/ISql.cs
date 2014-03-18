using System.Collections.Generic;

namespace Cartisan.Repository {
    public interface ISql {
        IEnumerable<T> Execute<T>(string sql, params object[] parameters); 
    }
}