using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using PDX.Domain.Account;
using PDX.Domain.Common;
using PDX.Domain.Commodity;
using PDX.Domain.Customer;
using PDX.Domain.Document;
using PDX.DAL.Helpers;
using System.IO;

namespace PDX.DAL.Migrations
{
    public partial class MasterMigration : Migration
    {
        private string connectionString = "User ID=USER_ID;Password=PASSWORD;Host=localhost;Port=4321;Database=DATA_BASE_NAME;Pooling=true;";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            DbContextOptionsBuilder<PDXContext> options = new DbContextOptionsBuilder<PDXContext>();
            options.UseNpgsql(connectionString);

            //Delete existing records
            using (var context = new PDXContext(options.Options))
            {
                //Account                
                context.MenuRoles.Clear();
                context.UserRoles.Clear();
                context.Menus.Clear();
                context.Roles.Clear();
                context.Users.Clear();

                //Customer
                context.Agents.Clear();
                context.UserAgents.Clear();
                context.Manufacturers.Clear();

                //Commodity                
                context.Products.Clear();
                context.ProductManufacturers.Clear();

                //Document
                context.ModuleDocuments.Clear();
                context.LetterHeadings.Clear();
                context.Letters.Clear();

                //Lookups
                context.PaymentModes.Clear();
                context.ShippingMethods.Clear();
                context.PortOfEntries.Clear();
                context.Currencies.Clear();
                context.Countries.Clear();
                context.Addresses.Clear();
                context.AgentTypes.Clear();
                context.DocumentTypes.Clear();
                context.Modules.Clear();
                context.CommodityTypes.Clear();
                context.ImportPermitStatuses.Clear();
                context.ImportPermitDeliveries.Clear();
                context.UserTypes.Clear();
                context.AgentLevels.Clear();

                context.SaveChanges();
            }


            //Re Insert Records
            using (var context = new PDXContext(options.Options))
            {

                //lookups
                context.UserTypes.AddRange(GetUserTypes());
                context.PaymentModes.AddRange(GetPaymentModes());
                context.ShippingMethods.AddRange(GetShippingMethods());
                context.PortOfEntries.AddRange(GetPortOfEntries());
                context.Currencies.AddRange(GetCurrencies());
                context.Countries.AddRange(GetCountries());
                context.Addresses.AddRange(GetAddresses());
                context.AgentTypes.AddRange(GetAgentTypes());
                context.DocumentTypes.AddRange(GetDocumentTypes());
                context.Modules.AddRange(GetModules());
                context.CommodityTypes.AddRange(GetCommodityTypes());
                context.ImportPermitStatuses.AddRange(GetImportPermitStatus());
                context.ImportPermitDeliveries.AddRange(GetImportPermitDeliveries());
                context.AgentLevels.AddRange(GetAgentLevels());

                //Account
                context.Roles.AddRange(GetRoles());
                context.Menus.AddRange(GetMenus());
                context.MenuRoles.AddRange(GetMenuRoles());
                context.Users.AddRange(GetUsers());
                context.UserRoles.AddRange(GetUserRoles());

                //Customer
                context.Agents.AddRange(GetAgents());
                context.UserAgents.AddRange(GetUserAgents());
                context.Manufacturers.AddRange(GetManufacturers());

                //Commodity               
                context.Products.AddRange(GetProducts());
                context.ProductManufacturers.AddRange(GetProductManufacturers());


                //Document
                context.ModuleDocuments.AddRange(GetModuleDocuments());
                context.LetterHeadings.AddRange(GetLetterHeadings());
                context.Letters.AddRange(GetLetters());

                context.SaveChanges();

                //Excecute Views 
                //ExcecuteExternalSqlFiles(options);
            }
        }

        private void ExcecuteExternalSqlFiles(DbContextOptionsBuilder<PDXContext> options)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var path = currentPath.Replace("pdx.dal\\bin\\Debug\\netcoreapp1.1", "pdx.dal") + "\\Views";
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] files = dir.GetFiles("*.sql");
            using (var context = new PDXContext(options.Options))
            {
                foreach (var file in files)
                {
                    var script = string.Join(" ", File.ReadAllLines(file.FullName));
                    using (var connection = context.Database.GetDbConnection())
                    {
                        connection.Open();

                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = script;
                            var result = command.ExecuteScalar().ToString();
                        }
                    }
                }
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }


        private IList<Role> GetRoles()
        {
            var roles = new List<Role>{
                new Role(){ID = 1, Name="Admin",  RoleCode="ADM" },
                new Role(){ID = 2, Name="Customer Service Officer",  RoleCode="CSO"},
                new Role(){ID = 3, Name="Customer Service Director",  RoleCode="CSD"},
                new Role(){ID = 4, Name="Customer Service Team Leader",  RoleCode="CST"},
                new Role(){ID = 5, Name="Agent",  RoleCode="AGT"},
            };

            return roles;
        }

        private IList<UserType> GetUserTypes()
        {
            var list = new List<UserType>{
                new UserType(){ID = 1, Name="Staff",  UserTypeCode="STF" },
                new UserType(){ID = 2, Name="Company",  UserTypeCode="CMP"},
                new UserType(){ID = 3, Name="Agent",  UserTypeCode="AGN"},
                new UserType(){ID = 4, Name="Port Inspector",  UserTypeCode="PINS"},
                new UserType(){ID = 5, Name="TIPC",  UserTypeCode="TIPC"},
                new UserType(){ID = 6, Name="External",  UserTypeCode="EXT"},
            };

            return list;
        }

        private IList<Menu> GetMenus()
        {
            var menus = new List<Menu>{
                new Menu(){ID = 1, Name="Home",  MenuCode="HOM",Priority=1, URL="home", Icon="fa fa-home" },
                new Menu(){ID = 2, Name="Import Permit",  MenuCode="iPemit",Priority=2, URL="ipermit", Icon="fa fa-file-text-o" },
                new Menu(){ID = 3, Name="Admin",  MenuCode="ADM",Priority=5, URL="admin", Icon="fa fa-cogs" },
                new Menu(){ID = 4, Name="Manage Users",  MenuCode="USR",Priority=1, URL="users", Icon="fa fa-users" , ParentMenuID= 3},
                new Menu(){ID = 5, Name="Manage Roles",  MenuCode="ROL",Priority=2, URL="roles", Icon="fa fa-group" , ParentMenuID= 3},
                new Menu(){ID = 6, Name="Manage Documents",  MenuCode="DOC",Priority=3, URL="documents", Icon="fa fa-file-text" , ParentMenuID= 3},
                new Menu(){ID = 7, Name="Suppliers",  MenuCode="SUP",Priority=3, URL="suppliers", Icon="fa fa-building" },
                new Menu(){ID = 8, Name="Agents",  MenuCode="AGT",Priority=4, URL="suppliers", Icon="fa fa-adjust" }
            };

            return menus;
        }

        private IList<MenuRole> GetMenuRoles()
        {
            var menuRoles = new List<MenuRole>{
                     new MenuRole{ID=1,RoleID=3, MenuID=1},
                     new MenuRole{ID=2,RoleID=3, MenuID=2},
                     new MenuRole{ID=3,RoleID=2, MenuID=1},
                     new MenuRole{ID=4,RoleID=2, MenuID=2},
                     new MenuRole{ID=5,RoleID=1, MenuID=1},
                     new MenuRole{ID=6,RoleID=1, MenuID=2},
                     new MenuRole{ID=7,RoleID=1, MenuID=3},
                     new MenuRole{ID=8,RoleID=1, MenuID=4},
                     new MenuRole{ID=9,RoleID=1, MenuID=5},
                     new MenuRole{ID=10,RoleID=1, MenuID=6},
                     new MenuRole{ID=11,RoleID=1, MenuID=7},
                     new MenuRole{ID=12,RoleID=5, MenuID=8}
            };

            return menuRoles;
        }

        private IList<User> GetUsers()
        {

            var users = new List<User>{
                new User{ID=1, FirstName="admin", LastName="admin", Password="1000:6sx59KenGDFNZUYXo+/0o+KEtsYhQGg0:nAM+PIPVEKXHoTfkhjJ724EsDt4=", UserTypeID=1, Username="admin"},
                new User{ID=2, FirstName="cso", LastName="cso", Password="1000:6sx59KenGDFNZUYXo+/0o+KEtsYhQGg0:nAM+PIPVEKXHoTfkhjJ724EsDt4=", UserTypeID=1, Username="cso"},
                new User{ID=3, FirstName="test", LastName="test", Password="1000:6sx59KenGDFNZUYXo+/0o+KEtsYhQGg0:nAM+PIPVEKXHoTfkhjJ724EsDt4=", UserTypeID=1, Username="test"}

            };

            return users;
        }

        private IList<UserRole> GetUserRoles()
        {
            var list = new List<UserRole>{
                     new UserRole{ID=1,RoleID=1, UserID=1},
                     new UserRole{ID=2,RoleID=2, UserID=2},
                     new UserRole{ID=3,RoleID=3, UserID=3}
            };

            return list;
        }

        private IList<PaymentMode> GetPaymentModes()
        {
            var paymentMethods = new List<PaymentMode>{
                new PaymentMode{ID=1, Name="Letter of credit (L/C)", PaymentCode="LC", ShortName="LC"},
                new PaymentMode{ID=2, Name="Cash Against Document (CAD)", PaymentCode="CAD", ShortName="CAD"},
                new PaymentMode{ID=3, Name="Customer Payment Order ( CPO )", PaymentCode="CPO", ShortName="CPO"},
            };
            return paymentMethods;
        }

        private IList<ShippingMethod> GetShippingMethods()
        {
            var list = new List<ShippingMethod>{
                new ShippingMethod{ID=1, Name="By Sea", ShippingCode="SEA", ShortName="Sea"},
                new ShippingMethod{ID=2, Name="By Air", ShippingCode="AIR", ShortName="Air"}
            };
            return list;
        }

        private IList<PortOfEntry> GetPortOfEntries()
        {
            var list = new List<PortOfEntry>{
                new PortOfEntry{ID=1, Name="A.A Bole Air Port ", PortCode="AAB", ShippingMethodID=2, ShortName="Bole"},
                new PortOfEntry{ID=2, Name="Mojo Dry Port ", PortCode="MJO", ShippingMethodID=1, ShortName="Mojo"},
                new PortOfEntry{ID=3, Name="Kality Dry Port ", PortCode="KAL", ShippingMethodID=1, ShortName="Kality"},
            };
            return list;
        }


        private IList<Currency> GetCurrencies()
        {
            var list = new List<Currency>{
                new Currency{ID=1, Name="US Dollar", Symbol="$", ShortName="USD"},
                new Currency{ID=2, Name="Pound Sterling", Symbol="£", ShortName="Pound"},
                new Currency{ID=3, Name="Euro", Symbol="€", ShortName="Euro"},
            };
            return list;
        }

        private IList<CommodityType> GetCommodityTypes()
        {

            var list = new List<CommodityType>{
                new CommodityType{ID=1,CommodityTypeCode="PHAR",Name="Pharmaceuticals"}
            };
            return list;
        }
        private IList<Product> GetProducts()
        {

            var list = new List<Product>{
                new Product{ID=1,Name="TAMOXIFEN",GenericName="TAMOXIFEN",Description="",DosageForm="",DosageUnit="10 mg",DosageStrength="10"},
                new Product{ID=2,Name="IBUPROFEN",GenericName="IBUPROFEN",Description="",DosageForm="",DosageUnit="10 mg",DosageStrength="10"},
                new Product{ID=3,Name="ACYCLOVIR",GenericName="ACYCLOVIR",Description="",DosageForm="",DosageUnit="10 mg",DosageStrength="10"},
                new Product{ID=4,Name="CEFTRIAXONE",GenericName="CEFTRIAXONE",Description="",DosageForm="",DosageUnit="10 mg",DosageStrength="10"}

            };
            return list;
        }

        private IList<Country> GetCountries()
        {
            var list = new List<Country>
            {
                new Country{ID=1,Name="Ethiopia",CountryCode="ET"}
            };
            return list;
        }
        private IList<Address> GetAddresses()
        {

            var list = new List<Address>
            {
                new Address{ID=1,CountryID=1,Region="Addis Ababa"}
            };
            return list;
        }

        private IList<AgentType> GetAgentTypes()
        {

            var list = new List<AgentType>
            {
                new AgentType{ID=1,Name="Manufacturer", Description="Manufacturer", AgentTypeCode="MAN"},
                new AgentType{ID=2,Name="Importer", Description="Importer", AgentTypeCode="IMP"},
                new AgentType{ID=3,Name="Wholesaler", Description="Wholesaler", AgentTypeCode="WHS"},

            };
            return list;
        }
        private IList<AgentLevel> GetAgentLevels()
        {

            var list = new List<AgentLevel>
            {
                new AgentLevel{ID=1,Name="First Agent", Description="First Agent", AgentLevelCode="FAG"},
                new AgentLevel{ID=2,Name="Second Agent", Description="Second Agent", AgentLevelCode="SAG"}

            };
            return list;
        }
        private IList<Agent> GetAgents()
        {
            var list = new List<Agent>
            {
                new Agent{ID=1, Name="Agent 1", AgentTypeID=2, AddressID=1},
                new Agent{ID=2, Name="Agent 2", AgentTypeID=2, AddressID=1}
            };
            return list;
        }
        private IList<UserAgent> GetUserAgents()
        {
            var list = new List<UserAgent>
            {
                new UserAgent{ID=1,UserID=3,AgentID=1}
            };
            return list;
        }

        private IList<Module> GetModules()
        {
            var list = new List<Module>
            {
                new Module{ID=1,Name="Import Permit",ModuleCode="IPRM"}
            };
            return list;
        }

        private IList<DocumentType> GetDocumentTypes()
        {
            var list = new List<DocumentType>
            {
                new DocumentType{ID=1,Name="Official application letter",Description="Official application letter addressed to FMHACA", IsSystemGenerated=false},
                new DocumentType{ID=2,Name="Performa invoices",Description="Performa invoices if products are to be purchased", IsSystemGenerated=false},
                new DocumentType{ID=3,Name="Laboratory input",Description="Laboratory input receiving receipt and payment", IsSystemGenerated=false},
                 new DocumentType{ID=4,Name="Import Permit Application Acknowledgment Letter",Description="Import Permit Application Acknowledgment Letter", IsSystemGenerated=true}
            };
            return list;
        }

        private IList<ModuleDocument> GetModuleDocuments()
        {
            var list = new List<ModuleDocument>
            {
                new ModuleDocument{ID=1,SubmoduleID=1, DocumentTypeID=1, IsRequired = true},
                new ModuleDocument{ID=2,SubmoduleID=1, DocumentTypeID=2, IsRequired = true},
                new ModuleDocument{ID=3,SubmoduleID=1, DocumentTypeID=3, IsRequired = false},
                new ModuleDocument{ID=4,SubmoduleID=1, DocumentTypeID=4, IsRequired = false}
            };
            return list;
        }

        private IList<ProductManufacturer> GetProductManufacturers()
        {

            var list = new List<ProductManufacturer>{
            };
            return list;
        }
        private IList<ImportPermitStatus> GetImportPermitStatus()
        {
            var list = new List<ImportPermitStatus>
            {
                new ImportPermitStatus{ID=1,Name="Draft",Description="Draft", ImportPermitStatusCode="DRFT"},
                new ImportPermitStatus{ID=2,Name="Withdrawn",Description="Withdrawn", ImportPermitStatusCode="WITH"},
                new ImportPermitStatus{ID=3,Name="Requested",Description="Requested", ImportPermitStatusCode="RQST"},
                new ImportPermitStatus{ID=4,Name="Returned to Agent",Description="Returned to Agent", ImportPermitStatusCode="RTA"},
                new ImportPermitStatus{ID=5,Name="Returned to CSO",Description="Returned to CSO", ImportPermitStatusCode="RTC"},
                new ImportPermitStatus{ID=6,Name="Submitted for Approval",Description="Submitted for Approval", ImportPermitStatusCode="SFA"},
                new ImportPermitStatus{ID=7,Name="Submitted for Rejection",Description="Submitted for Rejection", ImportPermitStatusCode="SFR"},
                new ImportPermitStatus{ID=8,Name="Approved",Description="Approved", ImportPermitStatusCode="APR"},
                new ImportPermitStatus{ID=9,Name="Rejected",Description="Rejected", ImportPermitStatusCode="REJ"}
            };
            return list;
        }

        private IList<Manufacturer> GetManufacturers()
        {
            var list = new List<Manufacturer>
            {
                new Manufacturer{ID=1,Name="GSK",Description="GSK"}
            };
            return list;
        }

        private IList<ImportPermitDelivery> GetImportPermitDeliveries()
        {
            var list = new List<ImportPermitDelivery>
            {
                 new ImportPermitDelivery{ID=1,Name="Prompt",Description="Prompt"},
                 new ImportPermitDelivery{ID=2,Name="Within 30 days after receipt of LC",Description="Within 30 Days after Receipt of LC"},
                 new ImportPermitDelivery{ID=3,Name="Within 45 days after receipt of LC",Description="Within 45 Days after Receipt of LC"},
                 new ImportPermitDelivery{ID=4,Name="Within 60 days after receipt of LC",Description="Within 60 Days after Receipt of LC"},
                 new ImportPermitDelivery{ID=5,Name="Within 90 days after receipt of LC",Description="Within 90 Days after Receipt of LC"},
                 new ImportPermitDelivery{ID=6,Name="Within 120 days after receipt of LC",Description="Within 120 days after Receipt of LC"},
            };
            return list;
        }

        private IList<LetterHeading> GetLetterHeadings()
        {
            var list = new List<LetterHeading> {
                new LetterHeading{ID=1, CompanyName="FOOD, MEDICINE AND HEALTH CARE ADMINISTRATION AND CONTROL AUTHORITY OF ETHIOPIA", LogoUrl="http://res.cloudinary.com/USER_id/image/upload/v1491289619/FDRE_xmwryf.png"}
            };

            return list;
        }

        private IList<Letter> GetLetters()
        {
            var list = new List<Letter>{
                new Letter {
                            ID=1, 
                            ModuleDocumentID = 1, 
                            Title="PURCHASE ORDER APPLICATION ACKNOWLEDGMENT LETTER", 
                            Body=@"Your purchase order application for the below products have been accepted for evaluation. It is anticipated that the evaluation will be completed by approximately 3
                            days from the date of submission. The anticipated date of completion of the evaluation has been provided for your convenience and it is an estimate only.",
                            Footer="Please use the above application number for any future correspondence with FMHACA"}
            };

            return list;
        }

    }
}