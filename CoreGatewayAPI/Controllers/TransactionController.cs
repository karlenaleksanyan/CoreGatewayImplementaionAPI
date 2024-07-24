using CoreGateway.Abstraction;
using CoreGateway.Models.Models;
using Microsoft.AspNetCore.Mvc;
using static CoreGateway.Models.Enums.Enum;

namespace CoreGatewayAPI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly IService<TransactionModel> _transactionService;
        private readonly IService<ProductModel> _productService;

        public TransactionController(IService<TransactionModel> transactionService, IService<ProductModel> productService)
        {
            _transactionService = transactionService;
            _productService = productService;
        }

        /// <summary>
        /// Processes a transaction (purchase or sale).
        /// </summary>
        /// <param name="transactionModel">The transaction model.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ProcessTransaction([FromBody] TransactionModel transactionModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productService.GetFirstOrDefault(transactionModel.ProductId);
            if (product == null)
            {
                return NotFound("Product not found.");
            }

            switch (transactionModel.TransactionType)
            {
                case TransactionType.Purchase:
                    product.StockQuantity += transactionModel.Quantity;
                    break;

                case TransactionType.Sale:
                    if (product.StockQuantity < transactionModel.Quantity)
                    {
                        return BadRequest("Insufficient stock.");
                    }
                    product.StockQuantity -= transactionModel.Quantity;
                    break;

                default:
                    return BadRequest("Invalid transaction type.");
            }

            await _transactionService.Add(transactionModel);
            await _productService.Update(product);

            return Ok();
        }
    }
}