using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Model.Staging;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.DAL.Repositories;
using PDX.Domain.Staging;

namespace PDX.BLL.Services {
    public class MedicalDeviceStagingService : Service<MedicalDeviceHeader>, IMedicalDeviceStagingService {
        private List<string> columns = new List<string> {
            "CerteficateNumber",
            // "AgentName",
            // "SupplierName",
            // "ManufacturerName"
        };
        public MedicalDeviceStagingService (IUnitOfWork unitOfWork) : base (unitOfWork) { }
        public async Task<ApiResponse> SaveMedicalDeviceMigration (MedicalDeviceMigration mdMigrations) {
            //construct object
            MedicalDeviceHeader header = new MedicalDeviceHeader () {
                CerteficateNumber = mdMigrations.CerteficateNumber,
                RegistrationType = mdMigrations.RegistrationType,
                RegistrationDate = mdMigrations.RegistrationDate,
                ExpiryDate = mdMigrations.ExpiryDate,
                ManufacturerAddressID = mdMigrations.ManufacturerAddressID,
                AgentID = mdMigrations.AgentID,
                SupplierID = mdMigrations.SupplierID
            };

            ICollection<MedicalDeviceDetail> detail = new List<MedicalDeviceDetail> ();

            mdMigrations.Products.ToList ().ForEach (p => {
                p.ModelSizes.ToList ().ForEach (md => {
                md.PackSizes.ToList ().ForEach (ps => {

                MedicalDeviceDetail d = new MedicalDeviceDetail () {
                BrandName = p.BrandName,
                InnID = p.InnID,
                Description = p.Description,
                DeviceClassID = p.DeviceClassID,
                DeviceSize = md.Size,
                DeviceModel = md.Size,
                PackSizeID = ps.PackSizeID
                        };
                        detail.Add (d);

                    });
                });
            });

            header.Details = detail;
            //save 
            var result = await base.CreateOrUpdateAsync (header, true);
            return new ApiResponse {
                StatusCode = result ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                    IsSuccess = result,
                    Message = result ? "Migration created successfuly!" : "Error in creating migrations",
                    Result = result,
                    Type = typeof (bool).ToString ()
            };
        }

        public async Task<IEnumerable<MedicalDeviceMigration>> GetMedicalDeviceMigration () {
            //get objects
            var result = await base.GetAllAsync ();
            return MapMedicalDeviceMigration (result);
        }
        public async Task<DataTablesResult<MedicalDeviceMigration>> GetPageAsync (IDataTablesRequest request) {
            Expression<Func<MedicalDeviceHeader, bool>> predicate = DataTableHelper.ConstructFilter<MedicalDeviceHeader> (request.Search.Value, columns);
            OrderBy<MedicalDeviceHeader> orderBy = new OrderBy<MedicalDeviceHeader> (qry => qry.OrderBy (e => e.RegistrationDate));

            var sortableColumns = request.Columns.Where (c => c.Sort != null);
            var result = await base.GetPageAsync (request.Start, request.Length, sortableColumns, predicate, orderBy.Expression);
            var pageData = MapMedicalDeviceMigration (result);           

            var totalRecords = await base.CountAsync (predicate);
            var response = new DataTablesResult<MedicalDeviceMigration> (request.Draw, totalRecords, totalRecords, pageData);

            return response;
        }

        private IEnumerable<MedicalDeviceMigration> MapMedicalDeviceMigration (IEnumerable<MedicalDeviceHeader> medicalDevice) {

            List<MedicalDeviceMigration> mdMigrations = new List<MedicalDeviceMigration> ();

            medicalDevice?.ToList ().ForEach (md => {
                MedicalDeviceMigration migration = new MedicalDeviceMigration () {
                ID = md.ID,
                CerteficateNumber = md.CerteficateNumber,
                RegistrationType = md.RegistrationType,
                RegistrationDate = md.RegistrationDate,
                ExpiryDate = md.ExpiryDate,
                ManufacturerAddressID = md.ManufacturerAddressID,
                ManufacturerName = md.ManufacturerAddress.Manufacturer.Name,
                AgentID = md.AgentID,
                AgentName = md.Agent.Name,
                SupplierID = md.SupplierID,
                SupplierName = md.Supplier.Name,
                Products = new List<MedicalDeviceMigrationProduct> ()
                };

                // group distinct products
                var grouped = md.Details.GroupBy (detail =>
                    new {
                        ID = detail.ID,
                            BrandName = detail.BrandName,
                            InnID = detail.InnID,
                            GenericName = detail.Inn.Name,
                            Description = detail.Description,
                            DeviceClassID = detail.DeviceClassID,
                            DeviceClassName = detail.DeviceClass.Name,
                    }).Distinct ().Select (grp => new {
                    Product = grp.Key, ModelSizes = grp.ToList ()
                });

                foreach (var prd in grouped) {
                    var product = new MedicalDeviceMigrationProduct () {
                        ID = prd.Product.ID,
                        BrandName = prd.Product.BrandName,
                        InnID = prd.Product.InnID,
                        GenericName = prd.Product.GenericName,
                        Description = prd.Product.Description,
                        DeviceClassID = prd.Product.DeviceClassID,
                        DeviceClassName = prd.Product.DeviceClassName,
                        ModelSizes = new List<MedicalDeviceMigrationModelSize> ()
                    };
                    foreach (var modelSize in prd.ModelSizes) {
                        var mdSize = new MedicalDeviceMigrationModelSize () {
                            Size = modelSize.DeviceSize,
                            Model = modelSize.DeviceModel,
                            PackSizeID = modelSize.PackSizeID,
                            PackSizeName = modelSize.PackSize.Name,
                        };
                        product.ModelSizes.Add (mdSize);
                    }
                    migration.Products.Add (product);
                };
                mdMigrations.Add (migration);
            });

            return mdMigrations;
        }
    }
}