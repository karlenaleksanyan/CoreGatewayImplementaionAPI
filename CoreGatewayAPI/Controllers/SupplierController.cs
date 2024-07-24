using CoreGateway.Abstraction;
using CoreGateway.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreGatewayAPI.Controllers
{
    public class SupplierController : BaseController
    {
        private readonly IService<SupplierModel> _supplierService;

        public SupplierController(IService<SupplierModel> supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierModel>>> GetAll()
        {
            var suppliers = await _supplierService.GetAll();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierModel>> Get(Guid id)
        {
            var supplier = await _supplierService.GetFirstOrDefault(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult> Create(SupplierModel supplierModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _supplierService.Add(supplierModel);
            return CreatedAtAction(nameof(Get), new { id = supplierModel.Id }, supplierModel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(SupplierModel supplierModel)
        {
            var supplier = await _supplierService.GetFirstOrDefault(supplierModel.Id);
            if (supplier == null)
            {
                return NotFound();
            }

            await _supplierService.Update(supplierModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var supplier = await _supplierService.GetFirstOrDefault(id);
            if (supplier == null)
            {
                return NotFound();
            }

            await _supplierService.Delete(id);
            return NoContent();
        }
    }
}