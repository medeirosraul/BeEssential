using Beehouse.Essentials.Entities;
using System;

namespace Beehouse.Essentials.Types
{
    public class EntityConvertible<TEntity> where TEntity : Entity
    {
        public Func<TEntity> ToEntityExpression { get; set; }
        public Action<TEntity> FromEntityExpression { get; set; }

        public TEntity ToEntity()
        {
            return ToEntityExpression.Invoke();
        }
    }
}
