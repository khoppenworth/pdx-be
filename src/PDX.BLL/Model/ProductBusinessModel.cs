using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PDX.Domain.Commodity;

namespace PDX.BLL.Model {
    public class ProductBusinessModel {
        public string GenericName { get; set; }
        public int INNID { get; set; }
        public string BrandName { get; set; }
        public int? DosageFormID { get; set; }
        public int? DosageStrengthID { get; set; }
        public int? DosageUnitID { get; set; }
        public string ProductTypeCode { get; set; }
        public int? CreatedByUserID { get; set; }
        public int SupplierID { get; set; }
        public int ManufacturerAddressID { get; set; }
        public virtual List<MDModelSize> MDModelSizes { get; set; }
        public virtual List<Presentation> Presentations { get; set; }

    }
}