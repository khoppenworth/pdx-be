using System;
using System.Collections.Generic;
using System.Linq;
using PDX.BLL.Model;
using PDX.Domain.Commodity;
using PDX.Domain.Document;

namespace PDX.API.Model.Registration {
    public class RegistrationPDFData {
        public RegistrationPDFData (MABusinessModel ma, LetterHeading letterHeading, Letter letter, int noOfFIR = 1) {
            this.maId = ma.MA.ID;
            this.company = letterHeading.CompanyName;
            this.logoUrl = letterHeading.LogoUrl;
            this.header = letter.Title;
            this.body = letter.Body;
            this.to = ma.MA.Agent?.Name;
            this.address = ma.MA.Agent?.Address?.City;
            this.date = DateTime.Now.ToString ("dd-MM-yyyy");
            this.productName = ma.Product?.BrandName;
            this.genericName = ma.Product?.GenericName;
            this.dosageForm = ma.Product?.DosageFormObj?.Name;
            this.dosageUnit = ma.Product?.DosageUnitObj?.Name;
            this.productStrength = ma.Product?.DosageStrengthObj?.Name;
            this.routeAdminstration = ma.Product?.AdminRoute?.Name;
            this.indication = ma.Product?.Indication;
            this.prescriptionStatus = ma.Product?.PharmacopoeiaStandard?.Name; //check
            this.packSize = ma.Product?.Presentation; //check
            this.generalAppearance = ma.Product?.Description;
            this.primaryContainer = ma.Product?.ContainerType;
            this.agent = ma.MA.Agent?.Name;
            this.supplier = ma.MA.Supplier?.Name;
            this.supplierAddress = ma.MA.Supplier?.Address?.City;
            this.applicationType = ma.MA.MAType?.Name;
            this.applicationNumber = ma.MA.MANumber;
            this.certificationNumber = ma.MA.CertificateNumber; //check
            this.manufacturer = ma.Product?.ProductManufacturers?.DefaultIfEmpty ().FirstOrDefault (m => m?.ManufacturerType?.ManufacturerTypeCode == "FIN_PROD_MANUF")?.ManufacturerAddress.Manufacturer?.Name; //check
            this.shelfLife = ma.Product?.ShelfLifeObj?.Name;

            this.productCategory = ma.Product.ProductCategory?.Name;
            this.productType = ma.Product.ProductType?.Name;
            this.deviceClass = ma.Product.ProductMD?.DeviceClass?.Name;
            this.deviceGroup = ma.Product.ProductMD?.MDGrouping?.Name;
            this.foodIngredients = ma.Product.FoodIngredients;

            this.fileName = ma.MA.MAStatus.MAStatusCode == "FIR" ? noOfFIR + "_" + letter.ModuleDocument.DocumentType.Name + ".pdf" : letter.ModuleDocument.DocumentType.Name + ".pdf";
            this.tempFolderName = ma.MA.RowGuid.ToString ();
            this.manufacturers = (bool) ma.Product?.ProductManufacturers?.Any () ? ma.Product?.ProductManufacturers?.Select (x => new Manufacturer (x))?.ToList () : new List<Manufacturer> ();
            this.inns = (bool) ma.Product?.ProductCompositions?.Any () ? ma.Product?.ProductCompositions?.Where (c => c.INNID != null).Select (x => new Ingredient (x))?.ToList () : new List<Ingredient> ();
            this.expients = (bool) ma.Product?.ProductCompositions?.Any () ? ma.Product?.ProductCompositions?.Where (c => c.ExcipientID != null).Select (x => new Ingredient (x))?.ToList () : new List<Ingredient> ();
            this.components = (bool) ma.Product?.DeviceAccessories?.Any () ? ma.Product?.DeviceAccessories?.Where (c => c.AccessoryType.AccessoryTypeCode == "CMP").Select (x => new DeviceAccessories (x))?.ToList () : new List<DeviceAccessories> ();
            this.rawMaterials = (bool) ma.Product?.DeviceAccessories?.Any () ? ma.Product?.DeviceAccessories?.Where (c => c.AccessoryType.AccessoryTypeCode == "RMT").Select (x => new DeviceAccessories (x))?.ToList () : new List<DeviceAccessories> ();

            this.spareParts = (bool) ma.Product?.DeviceAccessories?.Any () ? ma.Product?.DeviceAccessories?.Where (c => c.AccessoryType.AccessoryTypeCode == "SPR").Select (x => new DeviceAccessories (x))?.ToList () : new List<DeviceAccessories> ();;
            this.standardAccessories = (bool) ma.Product?.DeviceAccessories?.Any () ? ma.Product?.DeviceAccessories?.Where (c => c.AccessoryType.AccessoryTypeCode == "STND").Select (x => new DeviceAccessories (x))?.ToList () : new List<DeviceAccessories> ();;
            this.mdModelSizes = (bool) ma.Product?.MDModelSizes?.Any () ? ma.Product?.MDModelSizes.Select (x => new MDModelSize (x))?.ToList () : new List<MDModelSize> ();;
            this.foodCompositions = (bool) ma.Product?.FoodCompositions?.Any () ? ma.Product?.FoodCompositions?.Where (c => c.CompositionType == "COMPOSITION").Select (x => new FoodComposition (x))?.ToList () : new List<FoodComposition> ();
            this.foodNutritions = (bool) ma.Product?.FoodCompositions?.Any () ? ma.Product?.FoodCompositions?.Where (c => c.CompositionType == "NUTRITIONAL_FACT").Select (x => new FoodComposition (x))?.ToList () : new List<FoodComposition> ();

       
        }
        public int maId { get; set; }
        public string company { get; set; }
        public string logoUrl { get; set; }
        public string header { get; set; }
        public string address { get; set; }
        public string to { get; set; }
        public string date { get; set; }
        public string body { get; set; }
        public string productName { get; set; }
        public string genericName { get; set; }
        public string dosageForm { get; set; }
        public string dosageUnit { get; set; }
        public string productStrength { get; set; }
        public string routeAdminstration { get; set; }
        public string indication { get; set; }
        public string prescriptionStatus { get; set; }
        public string packSize { get; set; }
        public string generalAppearance { get; set; }
        public string primaryContainer { get; set; }
        public string agent { get; set; }
        public string supplier { get; set; }
        public string supplierAddress { get; set; }
        public string review { get; set; }
        public string applicationType { get; set; }
        public string applicationNumber { get; set; }
        public string certificationNumber { get; set; }
        public string manufacturer { get; set; }
        public string shelfLife { get; set; }
        public string approvalDate { get; set; }
        public string certificateValidDate { get; set; }
        public string firGeneratedDate { get; set; }
        public string firDueDate { get; set; }
        public string applicationDate { get; set; }
        public string fileName { get; set; }
        public string tempFolderName { get; set; }
        public string productCategory { get; set; }
        public string productType { get; set; }
        public string deviceGroup { get; set; }
        public string deviceClass { get; set; }
        public string foodIngredients { get; set; }
        public List<Manufacturer> manufacturers { get; set; }
        public List<Ingredient> inns { get; set; }
        public List<Ingredient> expients { get; set; }
        public List<Checklist> deficiency { get; set; }
        public List<VariationDifference> variationChanges { get; set; }
        public List<MDModelSize> mdModelSizes { get; set; }
        public List<DeviceAccessories> components { get; set; }
        public List<DeviceAccessories> rawMaterials { get; set; }
        public List<DeviceAccessories> standardAccessories { get; set; }
        public List<DeviceAccessories> spareParts { get; set; }
        public List<FoodComposition> foodCompositions { get; set; }
        public List<FoodComposition> foodNutritions { get; set; }
    }

}