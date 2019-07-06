using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Http;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Repositories;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.Domain.Commodity;
using PDX.Domain.Common;
using PDX.Domain.Customer;
using PDX.Domain.License;
using PDX.Domain.Views;

namespace PDX.BLL.Services {
    public class ProductService : Service<Product>, IProductService {
        private const int MAX_PRODUCTS = 100;
        private readonly IService<AgentSupplier> _agentSupplierService;
        private readonly IService<SupplierProduct> _supplierProductService;
        private readonly IService<vwProduct> _vwProductService;
        private readonly IService<vwAllProduct> _vwAllProductService;

        private readonly IAgentService _agentService;
        private readonly IUserService _userService;
        private readonly IDocumentService _documentService;
        private readonly IService<MA> _maService;
        private readonly IService<MDModelSize> _mdModelSizeService;
        private readonly IService<Presentation> _presentationService;
        private readonly IService<ManufacturerType> _manufacturerTypeService;
        private readonly IService<ProductType> _productTypeService;
        private List<string> columns = new List<string> {
            "FullItemName",
            "BrandName",
            "ManufacturerName",
            "SupplierName",
            "AgentName",
            "MAStatusCode"
        };
        public ProductService (IUnitOfWork unitOfWork, IProductRepository productRepository, IService<AgentSupplier> agentSupplierService,
            IService<SupplierProduct> supplierProductService, IService<vwProduct> vwProductService,
            IAgentService agentService, IUserService userService, IDocumentService documentService,
            IService<MA> maService, IService<MDModelSize> mdPresentationService, IService<Presentation> presentationService,
            IService<ManufacturerType> manufacturerTypeService, IService<ProductType> productTypeService, IService<vwAllProduct> vwAllProductService) : base (unitOfWork, productRepository) {
            _agentSupplierService = agentSupplierService;
            _supplierProductService = supplierProductService;
            _vwProductService = vwProductService;
            _vwAllProductService = vwAllProductService;
            _agentService = agentService;
            _userService = userService;
            _documentService = documentService;
            _maService = maService;
            _mdModelSizeService = mdPresentationService;
            _presentationService = presentationService;
            _manufacturerTypeService = manufacturerTypeService;
            _productTypeService = productTypeService;
        }

        public async Task<ApiResponse> CreateProduct (ProductBusinessModel product) {
            //Check if model is not null
            if (product == null) {
                return new ApiResponse () {
                StatusCode = StatusCodes.Status400BadRequest,
                Result = false,
                IsSuccess = false,
                Message = "Empty Object"
                };
            }
            //Construct Data Model
            SupplierProduct supplierProduct = await ConstructProduct (product);
            //Create Product
            var result = await _supplierProductService.CreateAsync (supplierProduct);
            //Get Product
            var response = await _vwAllProductService.GetAsync (pr => pr.ID == supplierProduct.ProductID);
            //Construct Product Supplier
            return new ApiResponse () {
                StatusCode = result?StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                    Result = response,
                    IsSuccess = result,
                    Message = result? "Successfully Created Product!": "Unable to create product, Check your model and try again!"
            };
        }
        public async Task<List<Product>> GetProductByAgent (int agentID) {
            List<Product> products = new List<Product> ();
            var agentSuppliers = await _agentSupplierService.FindByAsync (a => a.AgentID == agentID);
            foreach (var aSup in agentSuppliers) {
                var prds = await _supplierProductService.FindByAsync (sp => sp.SupplierID == aSup.SupplierID);
                foreach (var p in prds) {
                    p.Product.RegistrationDate = p.RegistrationDate;
                    p.Product.ProductStatus = p.Product.RegistrationDate == null? "Not Registered": "Registered";
                    p.Product.ExpiryDate = p.ExpiryDate;
                }
                products.AddRange (prds.Select (p => p.Product));
            }

            return products.Distinct ().ToList ();
        }

        public async Task<IEnumerable<AgentSupplier>> GetAgentSupplierByProduct (int productID) {
            var supplierProduct = (await _supplierProductService.GetAsync (a => a.ProductID == productID));
            var ma = await _maService.GetAsync (m => m.ID == supplierProduct.MAID);
            if (ma == null) return null;

            var agentSuppliers = await _agentSupplierService.FindByAsync (sp => sp.SupplierID == ma.SupplierID && sp.IsActive);
            return agentSuppliers;
        }

        public async Task<List<Domain.Document.Document>> GetProductDocuments (int productID) {
            var supplierProduct = (await _supplierProductService.GetAsync (a => a.ProductID == productID));
            var ma = await _maService.GetAsync (m => m.ID == supplierProduct.MAID);
            if (ma == null) return null;

            var uploadedDocuments = await _documentService.GetDocumentAsync (ma.MAType.MATypeCode, ma.ID);
            uploadedDocuments = uploadedDocuments.Where (d => (new List<string> { "Leaflet", "PriPack", "SecPack" }).Contains (d.ModuleDocument.DocumentType.DocumentTypeCode)).ToList ();
            return uploadedDocuments;
        }

        public async Task<List<Product>> GetSupplierProduct (int supplierID) {
            var supplierProducts = await _supplierProductService.FindByAsync (a => a.SupplierID == supplierID);
            foreach (var p in supplierProducts) {
                p.Product.RegistrationDate = p.RegistrationDate;
                p.Product.ProductStatus = p.Product.RegistrationDate == null? "Not Registered": "Registered";

                p.Product.ExpiryDate = p.ExpiryDate;
            }
            var products = supplierProducts.Select (p => p.Product).ToList ();
            return products;
        }

        public async Task<List<Product>> GetSupplierProductForIPermit (int supplierID, string productTypeCode = null) {
            var supplierProducts = await _supplierProductService.FindByAsync (a => a.SupplierID == supplierID && a.IsActive && a.ExpiryDate > DateTime.Now &&
                (productTypeCode == null || (productTypeCode != null && a.Product.ProductType.ProductTypeCode == productTypeCode)));
            foreach (var p in supplierProducts) {
                p.Product.RegistrationDate = p.RegistrationDate;
                p.Product.ExpiryDate = p.ExpiryDate;
                p.Product.ProductStatus = p.Product.RegistrationDate == null? "Not Registered": "Registered";

                p.Product.Presentations = (await _presentationService.FindByAsync (pr => pr.ProductID == p.Product.ID)).ToList ();
            }
            var products = supplierProducts.Select (p => p.Product).ToList ();
            return products;
        }
        public async Task<IEnumerable<MDModelSize>> GetMDSupplierProductForIPermit (int supplierID, string productTypeCode = null) {
            var supplierProducts = await _supplierProductService.FindByAsync (a => a.SupplierID == supplierID && a.ExpiryDate > DateTime.Now &&
                (productTypeCode == null || (productTypeCode != null && a.Product.ProductType.ProductTypeCode == productTypeCode)));

            var productIDs = supplierProducts.Select (p => p.ProductID);
            var products = supplierProducts != null ? await _mdModelSizeService.FindByAsync (p => productIDs.Contains (p.ProductID)) : null;

            foreach (var p in products) {
                var sp = supplierProducts.FirstOrDefault (s => s.ProductID == p.ProductID);
                if (sp != null) {
                    p.Product.RegistrationDate = sp.RegistrationDate;
                    p.Product.ExpiryDate = sp.ExpiryDate;
                    p.Product.ProductStatus = p.Product.RegistrationDate == null? "Not Registered": "Registered";

                }
            }

            return products;
        }

        /// <summary>
        /// Search product
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<vwProduct>> SearchProduct (string query) {
            if (query.Length < 3) return null;

            Expression<Func<vwProduct, bool>> predicate = DataTableHelper.ConstructFilter<vwProduct> (query, new List<string> { "FullItemName", "BrandName" });
            OrderBy<vwProduct> orderBy = new OrderBy<vwProduct> (qry => qry.OrderBy (e => e.FullItemName));
            var response = (await _vwProductService.FindByAsync (predicate, orderBy.Expression)).Take (MAX_PRODUCTS).ToList ();
            return response.Where (p => p.IsExpired == null || !(bool) p.IsExpired).ToList ();
        }

        /// <summary>
        /// Search All unregistered and registered product
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<vwAllProduct>> SearchAllProduct (string query, string productTypeCode , string importPermitTypeCode, int? supplierID = null) {
            if (query != null && query.Length < 3) return null;

            Expression<Func<vwAllProduct, bool>> predicate = ConstructAllProductFilter (query, new List<string> { "FullItemName", "BrandName" }, productTypeCode, false, supplierID);

            OrderBy<vwAllProduct> orderBy = new OrderBy<vwAllProduct> (qry => qry.OrderBy (e => e.FullItemName));
            var response = (await _vwAllProductService.FindByAsync (predicate, orderBy.Expression)).Take (MAX_PRODUCTS).ToList ();

            Func<vwAllProduct, bool> filter = p => p.IsExpired == null || !(bool) p.IsExpired;
            Func<vwAllProduct, bool> filterForImportPermit = p => p.ProductStatus == "Registered" && p.IsExpired != null && !(bool) p.IsExpired; //, then filter out unregistered products

            // if importPermitTypeCode == "IPRM", the search request is for Import Permit
            return response.Where (importPermitTypeCode == "IPRM" ? filterForImportPermit : filter).ToList ();
        }

        /// <summary>
        /// Products page
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwProduct>> GetProductPage (IDataTablesRequest request, string submoduleTypeCode = null) {
            Expression<Func<vwProduct, bool>> predicate = ConstructFilter<vwProduct> (null, request.Search.Value, false,submoduleTypeCode);
            OrderBy<vwProduct> orderBy = new OrderBy<vwProduct> (qry => qry.OrderBy (e => e.BrandName));
            var response = await _vwProductService.GetPageAsync (request, predicate, orderBy.Expression);
            return response;
        }

        /// <summary>
        /// Products by agent page
        /// </summary>
        /// <param name="request"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<DataTablesResult<vwProduct>> GetProductByUserPage (IDataTablesRequest request, int userID, string submoduleTypeCode = null) {
            IList<int> suppliers = null;
            var user = await _userService.GetAsync (userID);
            var fRole = user.Roles.FirstOrDefault ();

            if (fRole.RoleCode == "IPA") {
                var agents = await _agentService.GetAgentByUser (userID);
                if (agents == null || agents.Count == 0) return null;

                suppliers = (await _agentSupplierService.FindByAsync (ua => ua.IsActive && ua.AgentID == agents.FirstOrDefault ().ID &&
                    ua.AgentLevel.AgentLevelCode == "FAG")).Select (asup => asup.SupplierID).ToList ();
            }
            Expression<Func<vwProduct, bool>> predicate = ConstructFilter<vwProduct> (suppliers, request.Search.Value, true, submoduleTypeCode);
            OrderBy<vwProduct> orderBy = new OrderBy<vwProduct> (qry => qry.OrderBy (e => e.BrandName));
            var response = await _vwProductService.GetPageAsync (request, predicate, orderBy.Expression);
            return response;
        }

        public async Task<Product> GetProductByMAAsync (int maID) {
            var supplierProduct = await _supplierProductService.GetAsync (sp => sp.MAID == maID);
            var product = supplierProduct != null ? await this.GetAsync (supplierProduct.ProductID) : null;
            return product;
        }
        public async Task<IEnumerable<MDModelSize>> GetProductsByMAAsync (int maID) {
            var supplierProduct = await _supplierProductService.FindByAsync (sp => sp.MAID == maID);
            var productIDs = supplierProduct.Select (p => p.ProductID);
            var products = supplierProduct != null ? await _mdModelSizeService.FindByAsync (p => productIDs.Contains (p.ProductID)) : null;
            return products;
        }

        private Expression<Func<T, bool>> ConstructFilter<T> (IList<int> suppliers = null, string search = null, bool isExpired = true, string submoduleTypeCode = null) {
            Expression<Func<T, bool>> expression = null;
            ParameterExpression argument = Expression.Parameter (typeof (T), "p");
            Expression predicateBody = null;

            if (suppliers != null && suppliers.Any ()) {
                predicateBody = argument.DynamicContains ("SupplierID", suppliers);
            }
            if (submoduleTypeCode != null) {
                var exp = argument.GetExpression ("SubmoduleTypeCode", submoduleTypeCode, "Equal", typeof (string));
                predicateBody = predicateBody == null ? exp : Expression.AndAlso (predicateBody, exp);
            }

            var e = argument.GetExpression ("MAStatusCode", "APR", "Equal", typeof (string));

            predicateBody = predicateBody == null ? e : Expression.AndAlso (predicateBody, e);

            //Search filter expression 
            if (!string.IsNullOrEmpty (search) && search.Length > 2) {
                Expression pb = null;
                foreach (var str in columns) {
                    var exp = argument.StringContains (str, search);
                    pb = pb == null ? exp : Expression.OrElse (pb, exp);
                }
                predicateBody = Expression.AndAlso (predicateBody, pb);
            }
            if (!isExpired) {
                var expired = Expression.OrElse (argument.GetExpression ("IsExpired", false, "Equal", typeof (bool?)), argument.GetExpression ("IsExpired", null, "Equal", typeof (bool?)));
                predicateBody = Expression.AndAlso (predicateBody, expired);
            }

            expression = Expression.Lambda<Func<T, bool>> (predicateBody, new [] { argument });
            return expression;
        }

        private Expression<Func<vwAllProduct, bool>> ConstructAllProductFilter (string search, IList<string> searchcolumns, string productTypeCode, bool activeOnly = false, int? supplierID = null) {
            ParameterExpression argument = Expression.Parameter (typeof (vwAllProduct), "t");
            Expression predicateBody = null;

            //Filter active products
            if (activeOnly) {
                predicateBody = argument.GetExpression ("IsActive", activeOnly, "Equal", typeof (bool));
            }
            if (supplierID != null) {
                Expression e = argument.GetExpression ("SupplierID", supplierID, "Equal", typeof (int?));
                predicateBody = predicateBody == null?e : Expression.AndAlso (predicateBody, e);
            }

            //Filter Product Type : Medicine, Medical Device
            Expression e1 = argument.GetExpression ("ProductTypeCode", productTypeCode, "Equal", typeof (string));
            predicateBody = predicateBody == null?e1 : Expression.AndAlso (predicateBody, e1);

            if (!string.IsNullOrEmpty (search) && search.Length > 2) {
                Expression pb = null;
                foreach (var str in columns) {
                    var exp = argument.StringContains (str, search);
                    pb = pb == null ? exp : Expression.OrElse (pb, exp);
                }
                predicateBody = predicateBody == null ? pb : Expression.AndAlso (predicateBody, pb);

            }
            Expression<Func<vwAllProduct, bool>> expression = predicateBody != null ? Expression.Lambda<Func<vwAllProduct, bool>> (predicateBody, new [] { argument }) : null;
            return expression;
        }

        private async Task<SupplierProduct> ConstructProduct (ProductBusinessModel product) {
            //Construct Product Data Model
            var productDto = new Product ();
            productDto.Name = product.BrandName;
            productDto.BrandName = product.BrandName;
            productDto.GenericName = product.GenericName;
            productDto.INNID = product.INNID;
            productDto.DosageFormID = product.DosageFormID;
            productDto.DosageStrengthID = product.DosageStrengthID;
            productDto.DosageUnitID = product.DosageUnitID;
            productDto.RowGuid = System.Guid.NewGuid ();
            productDto.CreatedByUserID = product.CreatedByUserID;

            //Load product type from code
            ProductType productType = await _productTypeService.GetAsync (mt => mt.ProductTypeCode == product.ProductTypeCode);
            productDto.ProductTypeID = productType.ID;

            //Construct product manufacturer
            ManufacturerType finishedGoodManufacturer = await _manufacturerTypeService.GetAsync (mt => mt.ManufacturerTypeCode == "FIN_PROD_MANUF");

            productDto.ProductManufacturers = new List<ProductManufacturer> () {
                new ProductManufacturer () {
                ManufacturerAddressID = product.ManufacturerAddressID,
                ManufacturerTypeID = finishedGoodManufacturer?.ID
                }
            };

            //Construct Product Presentation
            productDto.Presentations = product.Presentations;

            //Construct Product MD presentation if product type is medical device
            productDto.MDModelSizes = product.MDModelSizes;
            //Construct Supplier Product

            SupplierProduct supProduct = new SupplierProduct () {
                SupplierID = product.SupplierID,
                Product = productDto
            };
            return supProduct;
        }
    }
}