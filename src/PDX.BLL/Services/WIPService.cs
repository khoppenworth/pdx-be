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
using PDX.Domain.Common;

namespace PDX.BLL.Services
{
    public class WIPService : Service<WIP>, IWIPService
    {
        private IService<MAType> _maTypeService;
        public WIPService(IUnitOfWork unitOfWork, IService<MAType> maTypeService) : base(unitOfWork)
        {
            _maTypeService = maTypeService;
        }

        public override async Task<IEnumerable<WIP>> GetAllAsync(bool activeOnly = false, Func<IQueryable<WIP>, IOrderedQueryable<WIP>> orderBy = null)
        {
            var result = await base.GetAllAsync();
            foreach (var wip in result)
            {
                wip.ContentObject = await DeserializeWIP(wip.Type, wip.Content);
            }
            return result;
        }

        public override async Task<IEnumerable<WIP>> FindByAsync(Expression<Func<WIP, bool>> predicate, Func<IQueryable<WIP>, IOrderedQueryable<WIP>> orderBy = null)
        {
            var result = await base.FindByAsync(predicate, orderBy);
            foreach (var wip in result)
            {
                wip.ContentObject = await DeserializeWIP(wip.Type, wip.Content);
            }
            return result;
        }

        public override async Task<WIP> GetAsync(int ID, bool noTracking = true)
        {
            var result = await base.GetAsync(ID);
            result.ContentObject = await DeserializeWIP(result.Type, result.Content);
            return result;
        }
        public override async Task<WIP> GetAsync(Expression<Func<WIP, bool>> predicate, bool noTracking = true)
        {
            var result = await base.GetAsync(predicate);
            if (result != null)
            {
                result.ContentObject = await DeserializeWIP(result.Type, result.Content);
            }
            return result;
        }

        public override async Task<bool> CreateOrUpdateAsync(WIP entity, bool commit = true, Expression<Func<WIP, bool>> resolveBy = null)
        {
            //Serialize WIP Object into json string before saving
            entity.Content = JsonConvert.SerializeObject(entity.ContentObject);
            var result = await base.CreateOrUpdateAsync(entity, commit, x => x.RowGuid == entity.RowGuid);
            return result;
        }

        public async Task<bool> WIPContentExistAsync<WContent>(IList<string> types, Func<WContent, bool> predicate) where WContent : class
        {
            var wips = await base.FindByAsync(wip => types.Contains(wip.Type));
            IList<WContent> wContents = new List<WContent>();

            foreach (var wip in wips)
            {
                WContent wContent = DeserializeWIPContent<WContent>(wip.Content);
                wContents.Add(wContent);
            }
            return wContents.Any(predicate);
        }


        private async Task<dynamic> DeserializeWIP(string wipType, string content)
        {
            dynamic obj = null;
            if (wipType == "IPRM" || wipType == "PIP")
            {
                obj = DeserializeWIPContent<ImportPermitBusinessModel>(content);
                obj.ImportPermit.RowGuid = obj.Identifier;
            }
            else if ((new List<string> { "NMR", "REN", "VAR" }).Contains(wipType))
            {
                obj = DeserializeWIPContent<MABusinessModel>(content);
                obj.MA.RowGuid = obj.Identifier;
                
                if (obj.MA.MAType != null)
                {
                    var SubmoduleType = (await _maTypeService.GetAsync(obj.MA.MAType.ID)).SubmoduleType;
                    obj.MA.MAType.SubmoduleType = SubmoduleType;
                    obj.SubmoduleTypeCode = SubmoduleType.SubmoduleTypeCode;
                }

            }
            return obj;
        }

        private WContent DeserializeWIPContent<WContent>(string content) where WContent : class
        {
            WContent wContent = JsonConvert.DeserializeObject<WContent>(content, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            return wContent;
        }
    }
}