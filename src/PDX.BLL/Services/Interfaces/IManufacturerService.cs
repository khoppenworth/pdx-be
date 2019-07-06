using System.Collections.Generic;
using System.Threading.Tasks;
using PDX.BLL.Model;
using PDX.Domain.Customer;
using PDX.Domain.Views;

namespace PDX.BLL.Services.Interfaces
{
    public interface IManufacturerService:IService<Manufacturer>
    {
        Task<IEnumerable<Manufacturer>> GetProductManufacturer(int productID);
        Task<DataTablesResult<Manufacturer>> SearchManufacturer(string query, int pageNumber, int? pageSize = null);
        Task<IEnumerable<ManufacturerAddress>> GetManufacturerAddressByProduct(int productID);
        Task<DataTablesResult<vwManufacturerAddress>> SearchManufacturerAddress(string query, int pageNumber, int? pageSize = null);
    }
}