using CoreGateway.Abstraction;
using CoreGateway.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreGatewayAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IService<ProductModel> _productService;

        public ProductController(IService<ProductModel> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> Get(Guid id)
        {
            var product = await _productService.GetFirstOrDefault(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductModel productModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.Add(productModel);
            return CreatedAtAction(nameof(Get), new { id = productModel.Id }, productModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(ProductModel productModel)
        {
            var product = await _productService.GetFirstOrDefault(productModel.Id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.Update(productModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var product = await _productService.GetFirstOrDefault(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productService.Delete(id);
            return NoContent();
        }
    }
}