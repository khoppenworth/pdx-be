using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PDX.Domain;
using PDX.Domain.Account;
using PDX.Domain.Catalog;
using PDX.Domain.Commodity;
using PDX.Domain.Common;
using PDX.Domain.Customer;
using PDX.Domain.Document;
using PDX.Domain.Finance;
using PDX.Domain.License;
using PDX.Domain.Logging;
using PDX.Domain.Procurement;
using PDX.Domain.Public;
using PDX.Domain.Report;
using PDX.Domain.Staging;
using PDX.Domain.Views;

namespace PDX.DAL {
    public class PDXContext : DbContext {
        public PDXContext (DbContextOptions<PDXContext> options) : base (options) { }
        //Account
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuRole> MenuRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserAgent> UserAgents { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ReportRole> ReportRoles { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }

        //Commodity
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductManufacturer> ProductManufacturers { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<DosageUnit> DosageUnits { get; set; }
        public DbSet<DosageForm> DosageForms { get; set; }
        public DbSet<DosageStrength> DosageStrengths { get; set; }
        public DbSet<AdminRoute> AdminRoutes { get; set; }
        public DbSet<AgeGroup> AgeGroups { get; set; }
        public DbSet<PharmacologicalClassification> PharmacologicalClassifications { get; set; }
        public DbSet<PharmacopoeiaStandard> PharmacopoeiaStandards { get; set; }
        public DbSet<Excipient> Excipients { get; set; }
        public DbSet<INN> INNs { get; set; }
        public DbSet<ATC> ATCs { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ShelfLife> ShelfLifes { get; set; }
        public DbSet<UseCategory> UseCategories { get; set; }
        public DbSet<SRA> SRAs { get; set; }
        public DbSet<ProductComposition> ProductCompositions { get; set; }
        public DbSet<ProductATC> ProductATCs { get; set; }
        public DbSet<PackSize> PackSizes { get; set; }
        public DbSet<Presentation> Presentations { get; set; }
        public DbSet<TherapeuticGroup> TherapeuticGroups { get; set; }
        public DbSet<FastTrackingItem> FastTrackingItems { get; set; }
        public DbSet<MDPresentation> MDPresentations { get; set; }
        public DbSet<MDModelSize> MDModelSizes { get; set; }
        public DbSet<MDDevicePresentation> MDDevicePresentations { get; set; }
        public DbSet<MDGrouping> MDGroupings { get; set; }
        public DbSet<ProductMD> ProductMDs { get; set; }
        public DbSet<DeviceAccessories> DeviceAccessories { get; set; }
        public DbSet<DeviceClass> DeviceClasses { get; set; }
        public DbSet<DeviceClassSubmodule> DeviceClassSubmodules { get; set; }

        //Common
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AgentType> AgentTypes { get; set; }
        public DbSet<CommodityType> CommodityTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Submodule> Submodules { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<PortOfEntry> PortOfEntries { get; set; }
        public DbSet<ImportPermitStatus> ImportPermitStatuses { get; set; }
        public DbSet<ImportPermitType> ImportPermitType { get; set; }
        public DbSet<ShippingMethod> ShippingMethods { get; set; }
        public DbSet<ShipmentStatus> ShipmentStatuses { get; set; }
        public DbSet<ImportPermitDelivery> ImportPermitDeliveries { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<AgentLevel> AgentLevels { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<ResponderType> ResponderTypes { get; set; }
        public DbSet<OptionGroup> OptionGroups { get; set; }
        public DbSet<AnswerType> AnswerTypes { get; set; }
        public DbSet<FeeType> FeeTypes { get; set; }
        public DbSet<ChecklistType> ChecklistTypes { get; set; }
        public DbSet<SRAType> SRATypes { get; set; }
        public DbSet<ForeignApplicationStatus> ForeignApplicationStatuses { get; set; }
        public DbSet<ManufacturerType> ManufacturerTypes { get; set; }
        public DbSet<MAType> MATypes { get; set; }
        public DbSet<MAStatus> MAStatuses { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Sequence> Sequences { get; set; }

        //Customer
        public DbSet<Agent> Agents { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<AgentSupplier> AgentSuppliers { get; set; }
        public DbSet<SupplierProduct> SupplierProducts { get; set; }
        public DbSet<ManufacturerAddress> ManufacturerAddresses { get; set; }

        //Document
        public DbSet<Document> Documents { get; set; }
        public DbSet<ModuleDocument> ModuleDocuments { get; set; }
        public DbSet<LetterHeading> LetterHeadings { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<PrintLog> PrintLogs { get; set; }

        //Procurement
        public DbSet<ImportPermit> ImportPermits { get; set; }
        public DbSet<ImportPermitDetail> ImportPermitDetails { get; set; }
        public DbSet<ImportPermitLogStatus> ImportPermitLogStatuses { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentDetail> ShipmentDetails { get; set; }
        public DbSet<ShipmentLogStatus> ShipmentLogStatuses { get; set; }

        //Reports
        public DbSet<Report> Reports { get; set; }

        //Settings
        public DbSet<SystemSetting> SystemSettings { get; set; }

        //Logging
        public DbSet<StatusLog> StatusLogs { get; set; }

        //Finance
        public DbSet<SubmoduleFee> SubmoduleFees { get; set; }
        public DbSet<MAPayment> MAPayments { get; set; }

        //Catalog
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<SubmoduleChecklist> SubmoduleChecklists { get; set; }
        public DbSet<Option> Options { get; set; }

        //License
        public DbSet<MA> MAs { get; set; }
        public DbSet<MAChecklist> MAChecklists { get; set; }
        public DbSet<ForeignApplication> ForeignApplications { get; set; }
        public DbSet<MALogStatus> MALogStatuses { get; set; }
        public DbSet<MAAssignment> MAAssignments { get; set; }
        public DbSet<MAReview> MAReviews { get; set; }
        public DbSet<MAVariationSummary> MAVariationSummarys { get; set; }
        public DbSet<FieldSubmoduleType> FieldSubmoduleTypes { get; set; }
        public DbSet<MAFieldSubmoduleType> MAFieldSubmoduleTypes { get; set; }

        //Staging 
        public DbSet<MedicalDeviceHeader> MedicalDeviceHeaders { get; set; }
        public DbSet<MedicalDeviceDetail> MedicalDeviceDetails { get; set; }

        //public 
        public DbSet<WIP> WIPs { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        //Views
        public DbSet<vwImportPermit> vwImportPermits { get; set; }
        public DbSet<vwPIP> vwPIPs { get; set; }
        public DbSet<vwUser> vwUsers { get; set; }
        public DbSet<vwSupplier> vwSuppliers { get; set; }
        public DbSet<vwAgent> vwAgents { get; set; }
        public DbSet<vwProduct> vwProducts { get; set; }
        public DbSet<vwAllProduct> vwAllProducts { get; set; }
        public DbSet<vwMA> vwMAs { get; set; }
        public DbSet<vwManufacturerAddress> vwManufacturerAddresses { get; set; }
        public DbSet<vwShipment> vwShipments { get; set; }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {

            modelBuilder.Entity<Sequence> ().Ignore (t => t.CreatedDate);
            modelBuilder.Entity<Sequence> ().Ignore (t => t.ModifiedDate);
            modelBuilder.Entity<Sequence> ().Ignore (t => t.IsActive);
            modelBuilder.Entity<Sequence> ().Ignore (t => t.RowGuid);

            modelBuilder.Entity<Role> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<User> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Menu> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MenuRole> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<UserRole> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<UserSubmoduleType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<UserAgent> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Permission> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<RolePermission> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ReportRole> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<UserLogin> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<Product> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ProductManufacturer> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ProductCategory> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<DosageUnit> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<DosageForm> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<DosageStrength> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<AdminRoute> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<AgeGroup> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<PharmacologicalClassification> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<PharmacopoeiaStandard> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Excipient> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<INN> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ATC> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ProductType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ShelfLife> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<UseCategory> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<SRA> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ProductComposition> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ProductATC> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<PackSize> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Presentation> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<TherapeuticGroup> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<FastTrackingItem> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MDDevicePresentation> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<Address> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<AgentType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<CommodityType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Country> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Currency> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<DocumentType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Module> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Submodule> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<PaymentMode> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<PortOfEntry> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ImportPermitStatus> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ImportPermitType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ShippingMethod> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ImportPermitDelivery> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<UserType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<AgentLevel> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ReportType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<PaymentTerm> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ResponderType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<OptionGroup> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<AnswerType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<FeeType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ChecklistType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<SRAType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ForeignApplicationStatus> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ManufacturerType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAStatus> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Field> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ShipmentStatus> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAFieldSubmoduleType> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<Agent> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Manufacturer> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Supplier> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<AgentSupplier> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<SupplierProduct> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ManufacturerAddress> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<Document> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ModuleDocument> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<LetterHeading> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Letter> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<PrintLog> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<ImportPermit> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ImportPermitDetail> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ImportPermitLogStatus> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Shipment> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ShipmentDetail> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ShipmentLogStatus> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<Report> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<SystemSetting> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<StatusLog> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<SubmoduleFee> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAPayment> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<Checklist> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<SubmoduleChecklist> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Option> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<MA> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAChecklist> ().Property (p => p.RowGuid).Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAChecklist> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAChecklist> ().Property (p => p.RowGuid).Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ForeignApplication> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MALogStatus> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAAssignment> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAReview> ().Property (p => p.RowGuid).Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MAReview> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<MedicalDeviceHeader> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<MedicalDeviceDetail> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<WIP> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<ChangeLog> ().Property (p => p.RowGuid).Metadata.AfterSaveBehavior = PropertySaveBehavior.Ignore;
            modelBuilder.Entity<Notification> ().Property (p => p.RowGuid).Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;

            modelBuilder.Entity<Notification> ()
                .Property<string> ("data")
                .HasField ("_data");
            modelBuilder.Entity<Report> ()
                .Property<string> ("column_definitions")
                .HasField ("_columnDefinitions");
            modelBuilder.Entity<Report> ()
                .Property<string> ("filter_columns")
                .HasField ("_filterColumns");
        }
    }
}