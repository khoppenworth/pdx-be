using System.Threading.Tasks;
using System.Collections.Generic;
using PDX.BLL.Services.Interfaces;
using PDX.Domain.Account;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using System;

namespace PDX.BLL.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly IWIPService _wipService;
        private readonly IImportPermitService _importPermitService;
        private readonly IMAService _maService;

        public UtilityService(IWIPService wipService, IImportPermitService importPermitService, IMAService maService)
        {
            _wipService = wipService;
            _importPermitService = importPermitService;
            _maService = maService;
        }

        public async Task<Guid> GenerateIdentifier(string key)
        {
            var identifier = System.Guid.NewGuid();
            var isUnique = !(await _wipService.ExistsAsync(w => w.RowGuid == identifier));

            switch (key)
            {
                case "ImportPermit":
                    isUnique = isUnique && !(await _importPermitService.ExistsAsync(ip => ip.RowGuid == identifier));
                    break;
                
                case "MarketAuthorization":
                    isUnique = isUnique && !(await _maService.ExistsAsync(ma => ma.RowGuid == identifier));
                    break;
            }

            return isUnique ? identifier : await GenerateIdentifier(key);
        }
    }
}