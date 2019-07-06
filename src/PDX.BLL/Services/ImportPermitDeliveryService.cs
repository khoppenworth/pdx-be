using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Repositories;
using PDX.Domain.Common;

namespace PDX.BLL.Services
{
    public class ImportPermitDeliveryService: Service<PDX.Domain.Common.ImportPermitDelivery>, IImportPermitDeliveryService
    {
        public ImportPermitDeliveryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<ImportPermitDelivery>> GetAllAsync(){
            var result = await base.GetAllAsync(false,x=>x.OrderByDescending(c=>c.Name));
            return result;
        }
    }
}