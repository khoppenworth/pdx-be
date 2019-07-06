using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.Domain.Common;



namespace PDX.BLL.Services.Interfaces
{
    public interface IImportPermitDeliveryService:IService<PDX.Domain.Common.ImportPermitDelivery>
    {
        Task<IEnumerable<ImportPermitDelivery>> GetAllAsync();
    }
}