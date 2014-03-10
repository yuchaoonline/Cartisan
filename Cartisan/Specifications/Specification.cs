﻿using System;
using System.Linq.Expressions;

namespace Cartisan.Specifications {
    public abstract class Specification<T>: ISpecification<T> {
        public static Specification<T> Eval(Expression<Func<T, bool>> expression) {
            return new ExpressionSpecification<T>(expression);
        } 

        public virtual bool IsSatisfiedBy(T obj) {
            return this.GetExpression().Compile()(obj);
        }

        public ISpecification<T> And(ISpecification<T> other) {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> Or(ISpecification<T> other) {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> AndNot(ISpecification<T> other) {
            return new AndNotSpecification<T>(this, other);
        }

        public ISpecification<T> Not() {
            return new NotSpecification<T>(this);
        }

        public abstract Expression<Func<T, bool>> GetExpression();
    }
}