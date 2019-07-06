using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using DataTables.AspNet.Core;

using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Commodity;
using PDX.Domain.Customer;
using PDX.Domain.Views;
using PDX.BLL.Model;
using PDX.BLL.Helpers;
using PDX.DAL.Query;
using PDX.Domain.Procurement;
using PDX.Domain.Logging;
using PDX.Domain;
using System.Reflection;

namespace PDX.BLL.Services
{
    public class StatusLogService : Service<StatusLog>, IStatusLogService
    {
        public StatusLogService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<bool> LogStatusAsync(BaseEntity oldEntity, BaseEntity newEntity, string fieldName, string comment, int modifiedBy)
        {
            var oldValue = Convert.ToString(oldEntity.GetValue(fieldName));
            var newValue = Convert.ToString(newEntity.GetValue(fieldName));

            //Nothing to log 
            if (oldValue == newValue) return false;

            System.Reflection.PropertyInfo p = oldEntity.GetType().GetProperty(fieldName);
            Type fieldType = p.PropertyType;

            var statusLog = new StatusLog{
                EntityType = oldEntity.GetType().Name,
                EntityID = oldEntity.ID,
                ColumnName = fieldName,
                ColumnType = fieldType.Name,
                OldValue = oldValue,
                NewValue = newValue,
                Comment = comment,
                ModifiedByUserID = modifiedBy
            };

            var result = await this.CreateAsync(statusLog);
            return result;

        }

        /// <summary>
        ///  Get statusLog by entity type and id
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="entityID"></param>
        /// <returns></returns>
        public async Task<IList<StatusLog>> GetStatusLogByEntityID(string entityType, int entityID){
            var statusLogs = await this.FindByAsync(sl => sl.EntityType == entityType && sl.EntityID == entityID);
            return statusLogs.OrderByDescending(o => o.CreatedDate).ToList();
        }
    }
}