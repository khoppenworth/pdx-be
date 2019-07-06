using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using PDX.BLL.Model;

namespace PDX.BLL.Helpers.ObjectDiffer
{
    public abstract class EnumerableDiffer : ITypeDiffer
    {
        public bool CanPerformDiff(Type t)
        {
            return typeof (IEnumerable).IsAssignableFrom(t);
        }

        public Difference PerformDiff(object newObj, object oldObj, string propName, Type type, Func<object, object, string, Type, string[], Difference> diffChildCallback, params string[] ignoreList)
        { 
            var newArray = newObj == null ? new List<object>() : (newObj as IEnumerable).Cast<object>().ToList();
            var oldArray = oldObj == null ? new List<object>() : (oldObj as IEnumerable).Cast<object>().ToList();

            var parentDifference = new Difference(propName, type.Name, newObj, oldObj)
            {
                ChildDiffs = GroupEqualObjects(newArray, oldArray)
                    .Select(
                        change =>
                            diffChildCallback(change.Item1, change.Item2, "Item", GetEnumerableElementType(type), null))
                    .Where(d => d != null)
                    .ToList()
            };
            // assume arrays are the same if there are no child differences
            return parentDifference.ChildDiffs.Any() ? parentDifference : null;
        }

        protected abstract IEnumerable<Tuple<object, object>> GroupEqualObjects(IEnumerable<object> newArray, IEnumerable<object> oldArray);

        private Type GetEnumerableElementType(Type type)
        {
            var enumerableType = type.GetInterfaces()
                .Union(type.IsInterface ? new List<Type> { type } : Enumerable.Empty<Type>())
                .First(i => i.IsGenericType && i.GenericTypeArguments.Count() == 1 && typeof (IEnumerable).IsAssignableFrom(i));
            return enumerableType.GetGenericArguments().First();
        }
    }


    // diffs 2 enumerables based on the element at each index
    public class IndexEnumerableDiffer : EnumerableDiffer
    {
        protected override IEnumerable<Tuple<object, object>> GroupEqualObjects(IEnumerable<object> newArray, IEnumerable<object> oldArray)
        {
            var maxLength = Math.Max(newArray.Count(), oldArray.Count());
            return Enumerable.Range(0, maxLength)
                .Select(i =>
                {
                    var newObj = i < newArray.Count() ? newArray.ElementAt(i) : null;
                    var oldObj = i < oldArray.Count() ? oldArray.ElementAt(i) : null;
                    return new Tuple<object, object>(newObj, oldObj);
                });
        }
    }


    // diffs 2 enumerables by comparing the elements of each enumerable based on their equality as determined by the _sameObjectComparer
    // ignores element indices
    public class ObjectEqualityEnumerableDiffer : EnumerableDiffer
    {
        private readonly IEqualityComparer<object> _sameObjectComparer;

        public ObjectEqualityEnumerableDiffer(IEqualityComparer<object> sameObjectComparer)
        {
            _sameObjectComparer = sameObjectComparer;
        }


        protected override IEnumerable<Tuple<object, object>> GroupEqualObjects(IEnumerable<object> newArray, IEnumerable<object> oldArray)
        {
            return newArray.Union(oldArray).Distinct(_sameObjectComparer)
                .Select(elem => new
                {
                    newItem = newArray.FirstOrDefault(x => _sameObjectComparer.Equals(x, elem)),
                    oldItem = oldArray.FirstOrDefault(x => _sameObjectComparer.Equals(x, elem))
                })
                .Select(x => new Tuple<object, object>(x.newItem, x.oldItem));
        }
    }

    // diffs any 2 objects based on their public properties
    public class ObjectTypeDiffer : ITypeDiffer
    {
        public bool CanPerformDiff(Type t)
        {
            // can perform diff on anything
            return true;
        }

        public Difference PerformDiff(object newObj, object oldObj, string propName, Type type, Func<object, object, string, Type, string[], Difference> diffChildCallback, params string[] ignoreList)
        {
            var parentDifference = new Difference(propName, type.Name, newObj, oldObj)
            {
                ChildDiffs = type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(prop =>
                (ignoreList == null || ignoreList !=null && !ignoreList.Contains(prop.Name) )
                && !prop.IsDefined(typeof(JsonIgnoreAttribute))).Select(
                    p =>
                    {
                        var newValue = newObj == null ? null : p.GetValue(newObj, null);
                        var oldValue = oldObj == null ? null : p.GetValue(oldObj, null);
                        return diffChildCallback(newValue, oldValue, p.Name, p.PropertyType, null);
                    })
                    .Where(d => d != null)
                    .ToList()
            };
            // assume objects are the same if there are no child differences
            return parentDifference.ChildDiffs.Any() ? parentDifference : null;
        }
    }

     // used for diffing primatives, and structs/objects that should be treated as primatives
    public class PrimativeDiffer : ITypeDiffer
    {
        public bool CanPerformDiff(Type t)
        {
            return t.IsPrimitive 
                || t == typeof(DateTime)
                || t == typeof(DateTime?)
                || t == typeof(int?)
                || t == typeof(string); // treat DateTime's as primatives, because the "Date" property has a circular reference to itself, which causes a stack overflow
        }

        public Difference PerformDiff(object newObj, object oldObj, string propName, Type type, Func<object, object, string, Type, string[], Difference> diffChildCallback, params string[] ignoreList)
        {
            // "cast" both objects to dynamic so their actual types are determined at runtime, and the "==" operator works as expected
            return ((dynamic) newObj) == ((dynamic) oldObj) ? null : new Difference(propName, type.Name, newObj, oldObj);
        }
    }

     public class DefaultEqualityComparer : IEqualityComparer<object>
    {
        public bool Equals(object x, object y)
        {
            return Object.Equals(x, y);
        }

        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}