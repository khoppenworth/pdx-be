using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Collections.Generic;
using System;
using PDX.BLL.Helpers.ExpressionPredicateBuilder;

namespace PDX.BLL.Helpers
{
    public static class ReflectionHelper
    {
        public static void CopyProperties(this object destination, object source)
        {
            if (source == null) return;
            // Iterate the Properties of the destination instance and  
            // populate them from their source counterparts  
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties(
                                                    BindingFlags.FlattenHierarchy |
                                                    BindingFlags.Public |
                                                    BindingFlags.Instance).Where(p =>
                                                                                      !p.IsDefined(typeof(NotMappedAttribute), true)
                                                                                   && !p.IsDefined(typeof(KeyAttribute), true)).ToArray();
            foreach (PropertyInfo destinationPi in destinationProperties)
            {
                PropertyInfo sourcePi = source.GetType().GetProperty(destinationPi.Name);

                if (sourcePi == null) continue;

                var sourceValue = sourcePi.GetValue(source, null);
                var destinationValue = destinationPi.GetValue(destination, null);

                if (destinationPi.CanWrite && sourceValue != null && sourceValue != destinationValue)
                {
                    destinationPi.SetValue(destination, sourceValue, null);
                }
            }
        }

        /// <summary>
        /// Get value of an object by its field name
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static object GetValue(this object source, string fieldName)
        {
            if (source == null) return null;
            PropertyInfo sourcePi = source.GetType().GetProperty(fieldName);
            if (sourcePi == null) return null;

            var sourceValue = sourcePi.GetValue(source, null);
            return sourceValue;
        }

        public static Expression GetExpression(this ParameterExpression argument, string property, object propValue, string method, System.Type type)
        {
            var left = Expression.Property(argument, property);
            var right = Expression.Constant(propValue, type);
            Expression e = ConstructExpression(method, left, right);
            return e;
        }

        public static Expression DynamicContains<TProperty>(this ParameterExpression argument, string property, IEnumerable<TProperty> items)
        {
            var propertyExp = Expression.Property(argument, property);
            var ce = Expression.Constant(items);

            var call = Expression.Call(typeof(Enumerable), "Contains", new[] { propertyExp.Type }, ce, propertyExp);

            return call;
        }

        public static Expression StringContains(this ParameterExpression argument, string property, string propertyValue)
        {
            var propertyExp = Expression.Property(argument, property);
            var toLower = Expression.Call(propertyExp, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var value = Expression.Constant(propertyValue.ToLower(), typeof(string));
            var call = Expression.Call(toLower, method, value);
            return call;
        }

        public static Expression BuildResolvingExpression<T>(T entity, params string[] resolvingProperties)
        {
            ParameterExpression argument = Expression.Parameter(typeof(T), "t");
            Expression predicateBody = null;

            foreach (var property in resolvingProperties)
            {
                var value = entity.GetValue(property);           
                Expression exp = argument.GetExpression(property, value, "Equal", value.GetType());
                predicateBody = predicateBody == null ? exp : Expression.AndAlso(predicateBody, exp);
            }
            Expression<Func<T, bool>> expression = predicateBody != null ? Expression.Lambda<Func<T, bool>>(predicateBody, new[] { argument }) : null;
            return expression;
        }
       


        static Expression ConstructExpression(string method, MemberExpression left, ConstantExpression right)
        {
            switch (method)
            {
                case "Equal":
                    return Expression.Equal(left, right);                   
                case "NotEqual":
                    return Expression.NotEqual(left, right);                   
            }

            return null;
        }
    }
}