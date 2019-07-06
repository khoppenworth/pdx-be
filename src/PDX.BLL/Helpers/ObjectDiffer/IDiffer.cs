using PDX.BLL.Model;

namespace PDX.BLL.Helpers.ObjectDiffer
{
    public interface IDiffer
    {
         Difference Diff<T>(T newObj, T oldObj, params string[] ignoreList);
    }
}