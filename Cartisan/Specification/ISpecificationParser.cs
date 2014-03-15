namespace Cartisan.Specification {
    public interface ISpecificationParser<T> {
        T Parse<TEntity>(ISpecification<TEntity> specification);
    }
}