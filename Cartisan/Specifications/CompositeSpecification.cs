namespace Cartisan.Specifications {
    public abstract class CompositeSpecification<T>: Specification<T>, ICompositeSpecification<T> {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        protected CompositeSpecification(ISpecification<T> left, ISpecification<T> right ) {
            this._left = left;
            this._right = right;
        }

        public ISpecification<T> Left {
            get { return this._left; }
        }

        public ISpecification<T> Right {
            get { return this._right; }
        }
    }
}