using Catolog.API.Entities;
using Catolog.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catolog.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger)
        {
                _repository = repository ?? throw new ArgumentNullException(nameof(repository));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}",Name ="GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var result =await _repository.GetProductById(id);
            if(result == null)
            {
                _logger.LogError($"Product {id} Not Found.");
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [Route("[action]/{category}",Name = "GetProductByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>),(int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var result = await _repository.GetproductsByCategory(category);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product),(int)HttpStatusCode.OK)]

        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {

            await _repository.CreatedProduct(product);

            return CreatedAtRoute("GetProduct", new {id=product.Id},product);
        }

    }
}
