using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.Domain.Common;
using PDX.Domain.Procurement;
using PDX.Domain.Views;

namespace PDX.BLL.Services {
    public class ShipmentService : Service<Shipment>, IShipmentService {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IService<ShipmentStatus> _shipmentStatusService;
        private readonly IService<vwShipment> _vwShipmentService;
        private readonly IService<ShipmentLogStatus> _shipmentLogStatusService;
        public ShipmentService (IUnitOfWork unitOfWork,
            IService<ShipmentStatus> shipmentStatusService, IService<vwShipment> vwShipmentService, IService<ShipmentLogStatus> shipmentLogStatusService) : base (unitOfWork) {
            _unitOfWork = unitOfWork;

            _shipmentStatusService = shipmentStatusService;
            _vwShipmentService = vwShipmentService;
            _shipmentLogStatusService = shipmentLogStatusService;
        }

        public async Task<ApiResponse> CreateShipmentAsync (ShipmentBusinessModel shipment) {
            var shipmentStatus = await _shipmentStatusService.GetAsync (ss => ss.ShipmentStatusCode == "RLSD");
            shipment.Shipment.ShipmentStatusID = shipmentStatus.ID;

            var result = await base.CreateAsync (shipment.Shipment);
            result = result && await SaveShipmentStatus (shipment);

            var apiResponse = new ApiResponse {
                StatusCode = result ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                IsSuccess = result,
                Message = result ? $"Shipment created successfully !" : $"Error in creating shipment",
                Result = result,
                Type = typeof (bool).ToString ()
            };
            return apiResponse;
        }
        // public async Task<ApiResponse> UpdateShipmentAsync(ShipmentBusinessModel shipment)
        // {

        // }
        public async Task<ShipmentBusinessModel> GetShipmentBusinessModel (int id) {
            var shipment = await base.GetAsync (id);

            var shipmentLogs = await _shipmentLogStatusService.FindByAsync (s => s.ShipmentID == id);

            var shipmentBusinessModel = new ShipmentBusinessModel {
                Shipment = shipment,
                ApplicationDate = (shipmentLogs.FirstOrDefault (f => f.ToShipmentStatus.ShipmentStatusCode == "RQST")).CreatedDate,
                InspectionStartDate = (shipmentLogs.FirstOrDefault (f => f.ToShipmentStatus.ShipmentStatusCode == "INST")).CreatedDate,
                InspectionEndDate = (shipmentLogs.FirstOrDefault (f => f.ToShipmentStatus.ShipmentStatusCode == "INSD")).CreatedDate,
                ReleasedDate = (shipmentLogs.FirstOrDefault (f => f.ToShipmentStatus.ShipmentStatusCode == "RLSD")).CreatedDate
            };

            return shipmentBusinessModel;
        }

        public async Task<DataTablesResult<vwShipment>> GetShipmentPageAsync (IDataTablesRequest request, int? userID = null) {
            Expression<Func<vwShipment, bool>> predicate = DataTableHelper.ConstructFilter<vwShipment> (request.Search.Value,
                new List<string> { "ImportPermitNumber", "ReleaseNumber", "ShipmentStatus", "PerformaInvoiceNumber", "AgentName", "SupplierName" });
            OrderBy<vwShipment> orderBy = new OrderBy<vwShipment> (qry => qry.OrderBy (e => e.ReleaseNumber));

            if (string.IsNullOrEmpty (request.Search.Value)) {
                predicate = null;
            }

            var response = await _vwShipmentService.GetPageAsync (request, predicate, orderBy.Expression);
            return response;
        }

        private async Task<bool> SaveShipmentStatus (ShipmentBusinessModel shipment) {

            var shipmentStatuses = await _shipmentStatusService.GetAllAsync ();

            //Applied
            var shipmentLogStatus1 = new ShipmentLogStatus {
                ShipmentID = shipment.Shipment.ID,
                ToStatusID = (shipmentStatuses.FirstOrDefault (sh => sh.ShipmentStatusCode == "RQST")).ID,
                Comment = $"New Shipment Created",
                CreatedDate = shipment.ApplicationDate,
                ModifiedDate = shipment.ApplicationDate,
                ModifiedByUserID = shipment.Shipment.CreatedByUserID
            };
            //Inspection Start Date
            var shipmentLogStatus2 = new ShipmentLogStatus {
                ShipmentID = shipment.Shipment.ID,
                ToStatusID = (shipmentStatuses.FirstOrDefault (sh => sh.ShipmentStatusCode == "INST")).ID,
                Comment = $"Shipment Inspection Started",
                CreatedDate = shipment.InspectionStartDate,
                ModifiedDate = shipment.InspectionStartDate,
                ModifiedByUserID = shipment.Shipment.CreatedByUserID
            };
            //Inspection End Date
            var shipmentLogStatus3 = new ShipmentLogStatus {
                ShipmentID = shipment.Shipment.ID,
                ToStatusID = (shipmentStatuses.FirstOrDefault (sh => sh.ShipmentStatusCode == "INSD")).ID,
                Comment = $"Shipment Inspection Ended",
                CreatedDate = shipment.InspectionEndDate,
                ModifiedDate = shipment.InspectionEndDate,
                ModifiedByUserID = shipment.Shipment.CreatedByUserID
            };
            //Released
            var shipmentLogStatus4 = new ShipmentLogStatus {
                ShipmentID = shipment.Shipment.ID,
                ToStatusID = (shipmentStatuses.FirstOrDefault (sh => sh.ShipmentStatusCode == "RLSD")).ID,
                Comment = $"Shipment Released",
                CreatedDate = shipment.ReleasedDate,
                ModifiedDate = shipment.ReleasedDate,
                ModifiedByUserID = shipment.Shipment.CreatedByUserID
            };

            var result = await _shipmentLogStatusService.CreateAsync (shipmentLogStatus1);
            result = result && await _shipmentLogStatusService.CreateAsync (shipmentLogStatus2);
            result = result && await _shipmentLogStatusService.CreateAsync (shipmentLogStatus3);
            result = result && await _shipmentLogStatusService.CreateAsync (shipmentLogStatus4);
            return result;
        }
    }
}