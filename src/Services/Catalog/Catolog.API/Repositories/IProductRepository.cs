using Catolog.API.Entities;

namespace Catolog.API.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetProductById(string id);

        Task<Product> GetProductByName(string name);

        Task<IEnumerable<Product>> GetproductsByCategory(string category);

        Task CreatedProduct(Product product);


        Task<bool> UpdateProducts(Product product);

        Task<bool?> UpdateProduct(Product product);
    }
}
