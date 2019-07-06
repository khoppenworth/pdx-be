using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.License;

namespace PDX.BLL.Services.Interfaces
{
    public interface IFieldService:IService<Field>
    {
         Task<IEnumerable<FieldSubmoduleType>> GetFieldByTypeAsync(bool? isVariationType = null,string submoduleTypeCode = "MED");  
         Task<IEnumerable<MAFieldSubmoduleType>> GetMAFieldByMA(int maID, bool? isVariationType = null,string submoduleTypeCode = "MED");           
         Task<IList<FieldSubmoduleType>> GetFieldByMA(int maID, bool? isVariationType = null,string submoduleTypeCode = "MED");
         Task<bool> SaveMAField(MAFieldSubmoduleType maField);  
         Task<bool> SaveMAField(IList<MAFieldSubmoduleType> maFields);   

    }
}