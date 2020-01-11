using Beehouse.Essentials.Entities;
using System;

namespace Beehouse.Essentials.Types
{
    public static class EntityConvertibleExtensions
    {
        public static TConvertible FromEntity<TConvertible, TEntity>(this TConvertible target, TEntity source)
            where TConvertible : EntityConvertible<TEntity>
            where TEntity : Entity
        {
            target.FromEntityExpression.Invoke(source);
            return target;
        }
    }
}
