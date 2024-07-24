using CoreGateway.Abstraction;
using CoreGateway.Models.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreGatewayAPI.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly IService<CategoryModel> _categoryService;

        public CategoryController(IService<CategoryModel> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetAll()
        {
            var categories = await _categoryService.GetAll();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> Get(Guid id)
        {
            var category = await _categoryService.GetFirstOrDefault(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CategoryModel categoryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoryService.Add(categoryModel);
            return CreatedAtAction(nameof(Get), new { id = categoryModel.Id }, categoryModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(CategoryModel categoryModel)
        {
            var category = await _categoryService.GetFirstOrDefault(categoryModel.Id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.Update(categoryModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await _categoryService.GetFirstOrDefault(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.Delete(id);
            return NoContent();
        }
    }
}