using System.Threading.Tasks;
using PDX.Domain.Catalog;

namespace PDX.BLL.Services.Interfaces
{
    public interface ISubmoduleChecklistService:IService<SubmoduleChecklist>
    {
         Task<bool> DeleteSubmoduleChecklistAsync(SubmoduleChecklist submoduleChecklist);
    }
}