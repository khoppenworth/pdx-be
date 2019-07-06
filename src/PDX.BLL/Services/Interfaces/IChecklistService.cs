using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PDX.BLL.Model;
using PDX.Domain.Catalog;
using PDX.Domain.License;

namespace PDX.BLL.Services.Interfaces
{
    public interface IChecklistService:IService<Checklist>
    {
         Task<IList<Checklist>> GetChecklistByTypeAsync(int checklistTypeID);

         Task<IEnumerable<Checklist>> GetChecklistByMAAsync(IEnumerable<MAChecklist>  maChecklist, string submoduleCode, string checklistTypeCode = null, bool? isSra = false);

         Task<IEnumerable<Checklist>> GetChecklistByMAAsync(int  maID, string submoduleCode, int userID, bool? isSra = false);
         Task<IEnumerable<Checklist>> GetChecklistByMAAsync(int  maID, string submoduleCode, string checklistTypeCode = null, bool? isSra = false);

         Task<IList<Checklist>> GetChecklistByModuleAsync(string moduleCode, string checklistTypeCode, bool? isSra = false);
         
         Task<IList<Checklist>> GetChecklistBySubmoduleAsync(string submoduleCode, string checklistTypeCode, bool? isSra = false,bool? isBothVariationType=false);

         Task<IEnumerable<MAReviewModel>> GetMAReviewWithChecklist(int  maID, string submoduleCode);

         Task<IEnumerable<Checklist>> GetPrescreeningDeficiency(int maID);
         Task<IEnumerable<Checklist>> GetChecklistBySubmoduleType (string checklistTypeCode, string submoduleTypeCode);
    }
}