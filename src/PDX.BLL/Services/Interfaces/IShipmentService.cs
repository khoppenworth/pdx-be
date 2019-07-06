using System.Threading.Tasks;
using DataTables.AspNet.Core;
using PDX.BLL.Model;
using PDX.Domain.Procurement;
using PDX.Domain.Views;

namespace PDX.BLL.Services.Interfaces
{
    public interface IShipmentService: IService<Shipment>
    {
         Task<ApiResponse> CreateShipmentAsync(ShipmentBusinessModel shipment);
         //Task<ApiResponse> UpdateShipmentAsync(ShipmentBusinessModel shipment);
         Task<ShipmentBusinessModel> GetShipmentBusinessModel(int id);
         Task<DataTablesResult<vwShipment>> GetShipmentPageAsync(IDataTablesRequest request, int? userID = null);

    }
}