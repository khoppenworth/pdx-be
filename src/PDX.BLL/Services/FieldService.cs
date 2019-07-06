using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.BLL.Helpers;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Helpers;
using PDX.DAL.Repositories;
using PDX.Domain.License;

namespace PDX.BLL.Services
{
    public class FieldService : Service<Field>, IFieldService
    {
        private readonly IService<FieldSubmoduleType> _fieldsubmoduleType;
        private readonly IService<MAFieldSubmoduleType> _maFieldSubmoduleType;
        public FieldService(IUnitOfWork unitOfWork,IService<FieldSubmoduleType> fieldsubmoduleType,IService<MAFieldSubmoduleType> maFieldSubmoduleType) : base(unitOfWork)
        {
            _fieldsubmoduleType = fieldsubmoduleType;
            _maFieldSubmoduleType = maFieldSubmoduleType;
        }

        // public async override Task<IEnumerable<Field>> GetAllAsync(bool activeOnly = false, Func<IQueryable<Field>, IOrderedQueryable<Field>> orderBy = null)
        // {
        //     var fields = await base.GetAllAsync(activeOnly, x => x.OrderBy(f => f.Priority));
        //     var hFields = BuildHierarchy(fields, null);
        //     return hFields;
        // }

        public async Task<IEnumerable<FieldSubmoduleType>> GetFieldByTypeAsync(bool? isVariationType = null,string submoduleTypeCode = "MED")
        {
            // var fields = await base.FindByAsync(y => (isVariationType == null || (isVariationType != null && y.IsVariationType == isVariationType) && ) && (isFoodType == null || (isFoodType != null && y.IsFoodType == isFoodType)), x => x.OrderBy(f => f.Priority));
            // var hFields = BuildHierarchy(fields, null);
            var fields = (await _fieldsubmoduleType.FindByAsync(y => (isVariationType == null || (isVariationType != null && y.IsVariationType == isVariationType)) 
                                                                && y.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode, x => x.OrderBy(f => f.Field.Priority)));
            var hFields = BuildHierarchy(fields, null);
            return hFields;
        }

        public async Task<IEnumerable<MAFieldSubmoduleType>> GetMAFieldByMA(int maID, bool? isVariationType = null,string submoduleTypeCode = "MED")
        {
            // var maFields = await _maFieldService.FindByAsync(mf => mf.MAID == maID && mf.IsActive && (isVariationType == null || (isVariationType != null && mf.IsVariation == isVariationType)) && (isFoodType == null || (isFoodType != null && mf.Field.IsFoodType == isFoodType)));
           var maFields = await _maFieldSubmoduleType.FindByAsync(mf => mf.MAID == maID && mf.IsActive && (isVariationType == null || (isVariationType != null && mf.IsVariation == isVariationType)) &&  mf.FieldSubmoduleType.SubmoduleType.SubmoduleTypeCode == submoduleTypeCode);
            return maFields;
        }
        public async Task<IList<FieldSubmoduleType>> GetFieldByMA(int maID, bool? isVariationType = null,string submoduleTypeCode = "MED")
        {
            var fields = (await GetMAFieldByMA(maID, isVariationType,submoduleTypeCode)).Select(s => s.FieldSubmoduleType).ToList();
            return fields;
        }

        public async Task<bool> SaveMAField(MAFieldSubmoduleType maField)
        {
            var existingMAFields = await _maFieldSubmoduleType.FindByAsync(mf => mf.MAID == maField.MAID);
            //Make inactive all existingMAFields
            foreach(var mf in existingMAFields){
                mf.IsActive = false;
            }
            await _maFieldSubmoduleType.UpdateAsync(existingMAFields);
            
            var result = await this.SaveNestedMAField(maField, maField.MAID, maField.IsVariation, maField.CreatedByUserID, true);
            return result;
        }

        public async Task<bool> SaveMAField(IList<MAFieldSubmoduleType> maFields)
        {
            var result = true;
            foreach (var maField in maFields)
            {
                result = result && (await this.SaveMAField(maField));
            }
            return result;
        }

        private async Task<bool> SaveNestedMAField(MAFieldSubmoduleType maField, int maID, bool isVariation, int createdByUserID, bool accumulator = true, bool commit = true)
        {
            if (maField.FieldSubmoduleType.IsSelected)
            {
                var tMAField = new MAFieldSubmoduleType();
                tMAField.CopyProperties(maField);
                tMAField.NullifyForeignKeys();

                accumulator = accumulator && (await _maFieldSubmoduleType.CreateOrUpdateAsync(tMAField, commit, mf => mf.MAID == tMAField.MAID && mf.FieldSubmoduleTypeID == tMAField.FieldSubmoduleTypeID && mf.IsVariation == tMAField.IsVariation));

                foreach (var child in maField.FieldSubmoduleType.Children.Where(f => f.IsSelected))
                {
                    var newMAField = new MAFieldSubmoduleType
                    {
                        MAID = maID,
                        FieldSubmoduleTypeID = child.ID,
                        FieldSubmoduleType = child,
                        IsVariation = isVariation,
                        CreatedByUserID = createdByUserID
                    };
                    await SaveNestedMAField(newMAField, maID, isVariation, createdByUserID, accumulator);
                }

                return accumulator;
            }
            return false;
        }


        /// <summary>
        /// Build Hierarchy of Fields in recurrsive approach
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parentFieldID"></param>
        /// <returns></returns>
        private IEnumerable<FieldSubmoduleType> BuildHierarchy(IEnumerable<FieldSubmoduleType> list, int? parentFieldID = null)
        {
            var hList = from li in list
                        where li.Field.ParentFieldID == parentFieldID
                        select new FieldSubmoduleType
                        {
                            ID = li.ID,
                            IsActive = li.IsActive,
                            CreatedDate = li.CreatedDate,
                            ModifiedDate = li.ModifiedDate,
                            RowGuid = li.RowGuid,

                            SubmoduleTypeID = li.SubmoduleTypeID,
                            FieldID = li.FieldID,
                            IsMajor = li.IsMajor,
                            IsVariationType = li.IsVariationType,
                            SubmoduleType = li.SubmoduleType,
                            Field = li.Field,
                            ParentFieldID = li.Field.ParentFieldID,

                            Children = BuildHierarchy(list, li.Field.ID)
                        };

            return hList.ToList();

        }


    }
}