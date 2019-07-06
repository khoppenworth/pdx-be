using System;

namespace PDX.BLL.Model
{
    public class ShipmentBusinessModel
    {
        public Domain.Procurement.Shipment Shipment { get; set; }
        public DateTime ApplicationDate { get; set; }
        public DateTime InspectionStartDate { get; set; }
        public DateTime InspectionEndDate { get; set; }
        public DateTime ReleasedDate { get; set; }
    }
}