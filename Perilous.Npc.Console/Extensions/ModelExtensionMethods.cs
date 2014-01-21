using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Perilous.Npc
{
    public static class ModelExtensionMethods
    {
        public static PropertyInfo GetProperty<TProperty>(this Expression<Func<TProperty>> property)
        {
            var expr = property.Body as MemberExpression;
            if (expr == null)
                throw new InvalidOperationException("Expression is not a member access expression.");

            var info = expr.Member as PropertyInfo;
            if (info == null)
                throw new InvalidOperationException("Member in expression is not a property.");

            return info;
        }

        public static PropertyInfo GetProperty<TModel, TProperty>(this TModel model,
            Expression<Func<TProperty>> property)
        {
            var expr = property.Body as MemberExpression;
            if (expr == null)
                throw new InvalidOperationException("Expression is not a member access expression.");

            var info = expr.Member as PropertyInfo;
            if (info == null)
                throw new InvalidOperationException("Member in expression is not a property.");

            return info;
        }
    }
}
