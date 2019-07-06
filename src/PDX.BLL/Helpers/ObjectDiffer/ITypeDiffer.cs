using System;
using PDX.BLL.Model;

namespace PDX.BLL.Helpers.ObjectDiffer
{
    public interface ITypeDiffer
    {
        bool CanPerformDiff(Type t);
        Difference PerformDiff(object newObj, object oldObj, string propName, Type type, Func<object, object, string, Type, string[], Difference> diffChildCallback, params string[] ignoreList);
    }
}