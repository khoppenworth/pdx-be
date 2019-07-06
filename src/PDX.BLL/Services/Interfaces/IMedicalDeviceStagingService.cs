using PDX.Domain.Staging;
using PDX.BLL.Model.Staging;
using PDX.DAL.Repositories;
using System.Threading.Tasks;
using PDX.BLL.Model;
using System.Collections.Generic;
using DataTables.AspNet.Core;
using PDX.BLL.Model;

namespace PDX.BLL.Services.Interfaces
{
    public interface IMedicalDeviceStagingService:IService<MedicalDeviceHeader>
    {
         Task<ApiResponse> SaveMedicalDeviceMigration(MedicalDeviceMigration medicalDeviceMigrations);
         Task<IEnumerable<MedicalDeviceMigration>> GetMedicalDeviceMigration();
         Task<DataTablesResult<MedicalDeviceMigration>> GetPageAsync (IDataTablesRequest request);
    }
}