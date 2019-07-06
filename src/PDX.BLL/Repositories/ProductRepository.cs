using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PDX.BLL.Helpers;
using PDX.DAL;
using PDX.DAL.Helpers;
using PDX.DAL.Repositories;
using PDX.Domain;
using PDX.Domain.Commodity;
using PDX.Logging;

namespace PDX.BLL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(PDXContext dbContext, ILogger logger) : base(dbContext, logger)
        {
        }

        public async override Task UpdateAsync(Product product)
        {
            try
            {
                
                var existingProduct = await base.GetAsync(product.ID);

                //Check if there are collections to be Removed
                var productCompositionsToRemove = existingProduct.ProductCompositions.Except(product.ProductCompositions, new IDComparer());
                var productATCsToRemove = existingProduct.ProductATCs.Except(product.ProductATCs, new IDComparer());
                var ProductManufacturersToRemove = existingProduct.ProductManufacturers.Except(product.ProductManufacturers, new IDComparer());
                var PresentationsToRemove = existingProduct.Presentations.Except(product.Presentations, new IDComparer());

                //Remove Collections
                RemoveCollection(productCompositionsToRemove);
                RemoveCollection(productATCsToRemove);
                RemoveCollection(ProductManufacturersToRemove);
                RemoveCollection(PresentationsToRemove);

                _dbSet.Update(product);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
            }

        }

    }
}