using System.Threading.Tasks;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Catalog;

namespace PDX.BLL.Services
{
    public class SubmoduleChecklistService : Service<SubmoduleChecklist>, ISubmoduleChecklistService
    {
        public SubmoduleChecklistService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// Override Created Method 
        /// </summary>
        /// <param name="submoduleChecklist"></param>
        /// <param name="commit"></param>
        /// <param name="createdBy"></param>
        /// <returns></returns>
        public async override Task<bool> CreateAsync(SubmoduleChecklist submoduleChecklist, bool commit = true, int? createdBy = null)
        {
            var result = await this.SaveNestedSubmoduleChecklist(submoduleChecklist.Checklist, submoduleChecklist.SubmoduleID, true, commit);
            return result;
        }

        /// <summary>
        /// Delete Nested SubmoduleChecklist
        /// </summary>
        /// <param name="submoduleChecklist"></param>
        /// <returns></returns>
        public async Task<bool> DeleteSubmoduleChecklistAsync(SubmoduleChecklist submoduleChecklist)
        {
            var result = await this.DeleteNestedSubmoduleChecklist(submoduleChecklist.Checklist, submoduleChecklist.SubmoduleID, true);
            return result;
        }



        private async Task<bool> SaveNestedSubmoduleChecklist(Checklist checklist, int submoduleID, bool accumulator = true, bool commit = true)
        {
            var submoduleChecklist = await base.GetAsync(sm => sm.ChecklistID == checklist.ID && sm.SubmoduleID == submoduleID);

            if (submoduleChecklist == null)
            {
                submoduleChecklist = new SubmoduleChecklist
                {
                    SubmoduleID = submoduleID,
                    ChecklistID = checklist.ID
                };
                accumulator = accumulator && (await base.CreateAsync(submoduleChecklist, commit));
            }
            //Save children relationship with the submodule
            foreach (var child in checklist.Children)
            {
                await SaveNestedSubmoduleChecklist(child, submoduleID, accumulator, commit);
            }

            return accumulator;
        }

        private async Task<bool> DeleteNestedSubmoduleChecklist(Checklist checklist, int submoduleID, bool accumulator = true, bool commit = true)
        {
            var submoduleChecklist = await base.GetAsync(sm => sm.ChecklistID == checklist.ID && sm.SubmoduleID == submoduleID);

            if (submoduleChecklist != null)
            {
                accumulator = accumulator && (await base.DeleteAsync(submoduleChecklist.ID, commit));
            }

            //Delete children
            foreach (var child in checklist.Children)
            {
                await DeleteNestedSubmoduleChecklist(child, submoduleID, accumulator, commit);
            }

            return accumulator;
        }
    }
}