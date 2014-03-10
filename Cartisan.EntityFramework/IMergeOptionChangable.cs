using System.Data.Entity.Core.Objects;
using Cartisan.Domain;

namespace Cartisan.EntityFramework {
    public interface IMergeOptionChangable {
        void ChangeMergeOption<TEntity>(MergeOption mergeOption) where TEntity: class, IAggregateRoot;
    }
}