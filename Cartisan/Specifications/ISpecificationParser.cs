using System.Runtime.InteropServices.ComTypes;

namespace Cartisan.Specifications {
    public interface ISpecificationParser<T> {
        T Parse<TEntity>(ISpecification<TEntity> specification);
    }
}