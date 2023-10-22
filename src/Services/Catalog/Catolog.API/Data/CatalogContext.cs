using Catolog.API.Entities;
using MongoDB.Driver;

namespace Catolog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSetting:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSetting:DatabaseName"));
            products = database.GetCollection<Product>(configuration.GetValue<string>("DatabaseSetting:CollectionName"));
            CatalogContextSeed.SeedData(products);
        }
        public IMongoCollection<Product> products { get; }
    }
}
