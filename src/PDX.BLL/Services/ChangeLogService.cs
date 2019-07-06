using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Public;

namespace PDX.BLL.Services
{
    public class ChangeLogService : Service<Domain.Public.ChangeLog>, IChangeLogService
    {
        public ChangeLogService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override async Task<IEnumerable<Domain.Public.ChangeLog>> GetAllAsync(bool activeOnly = false, Func<IQueryable<Domain.Public.ChangeLog>, IOrderedQueryable<Domain.Public.ChangeLog>> orderBy = null)
        {
            var result = await base.GetAllAsync(false, orderBy);
            foreach (var changeLog in result)
            {
                changeLog.ContentObject = DeserializeChangeLog(changeLog.Content);
            }
            return result;
        }

        public override async Task<Domain.Public.ChangeLog> GetAsync(int ID, bool noTracking = true)
        {
            var changeLog = await base.GetAsync(ID, noTracking);
            changeLog.ContentObject = DeserializeChangeLog(changeLog.Content);
            return changeLog;
        }

        public override async Task<bool> CreateOrUpdateAsync(Domain.Public.ChangeLog entity, bool commit = true, Expression<Func<Domain.Public.ChangeLog, bool>> resolveBy = null)
        {
            //serialize ChangeLog Object into json string before saving
            entity.Content = JsonConvert.SerializeObject(entity.ContentObject);
            var result = await base.CreateOrUpdateAsync(entity, commit, e => e.RowGuid == entity.RowGuid);
            return result;
        }


        private dynamic DeserializeChangeLog(string content)
        {
            dynamic obj = null;
            obj = JsonConvert.DeserializeObject<ChangeLogModel>(content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return obj;
        }
    }
}