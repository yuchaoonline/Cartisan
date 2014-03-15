namespace Cartisan.Specification {
    public interface ICompositeSpecification<T>: ISpecification<T> {
        ISpecification<T> Left { get; }
        ISpecification<T> Right { get; }
    }
}