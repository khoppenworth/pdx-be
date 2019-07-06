using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PDX.BLL.Helpers;
using PDX.BLL.Model;
using PDX.BLL.Services.Interfaces;
using PDX.DAL.Query;
using PDX.DAL.Repositories;
using PDX.Domain.Commodity;

namespace PDX.BLL.Services {
    public class ProductTypeService : Service<ProductType>, IProductTypeService {
        public ProductTypeService (IUnitOfWork unitOfWork) : base (unitOfWork) { }

        public override async Task<DataTablesResult<ProductType>> SearchAsync (SearchRequest searchRequest, bool activeOnly = false) {

            //Get searchable properties
            List<string> stringProperties = new List<string> () { "Name", "ProductTypeCode" };
            Expression<Func<ProductType, bool>> predicate = DataTableHelper.ConstructFilter<ProductType> (searchRequest.Query, stringProperties, activeOnly);
            
            if (string.IsNullOrEmpty (searchRequest.Query)) {
                predicate = null;
            }
            //submodule type and submodule filter            
            if (searchRequest.SubmoduleTypeCode != null) {
                Expression<Func<ProductType, bool>> submoduleTypeExpression = x => x.SubmoduleType.SubmoduleTypeCode == searchRequest.SubmoduleTypeCode;
                predicate = predicate != null? submoduleTypeExpression.AndAlso (predicate) : submoduleTypeExpression;

            };
            if (searchRequest.SubmoduleCode != null) {
                Expression<Func<ProductType, bool>> submoduleExpression = x => x.Submodule.SubmoduleCode == searchRequest.SubmoduleCode;
                predicate = predicate != null? submoduleExpression.AndAlso (predicate) : submoduleExpression;

            }
            OrderBy<ProductType> orderBy = new OrderBy<ProductType> (qry => qry.OrderBy (e => e.ID));

            var response = await SearchAsync (searchRequest, predicate, orderBy.Expression);
            return response;
        }
    }
}