using Amazon.Runtime.Internal.Util;
using Catolog.API.Data;
using Catolog.API.Entities;
using MongoDB.Driver;

namespace Catolog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext catalogContext;

        public ProductRepository(ICatalogContext catalog)
        {
            catalogContext = catalog ?? throw new ArgumentNullException(nameof(catalogContext));
        }
        public async Task CreatedProduct(Product product)
        {
           await catalogContext.products.InsertOneAsync(product); 
        }

        public async  Task<Product> GetProductById(string id)
        {
            return await catalogContext.products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.ElemMatch(p=>p.Name, name);
            return await catalogContext.products.Find(filterDefinition).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await  catalogContext.products.Find(p => true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetproductsByCategory(string category)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p=>p.Category, category);
            var result = await catalogContext.products.Find(filterDefinition).ToListAsync();
            return result;
        }

        public async Task<bool?> UpdateProduct(Product product)
        {

            var result = await catalogContext.products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> UpdateProducts(Product product)
        {
            var result = await catalogContext.products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}
