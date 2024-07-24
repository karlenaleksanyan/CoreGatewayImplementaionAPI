using CoreGateway.Abstraction;
using CoreGateway.DBMap.Models;
using CoreGateway.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreGatewayAPI.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IService<ProductModel> _productService; // Replace with your actual service

        public ReportController(IService<ProductModel> productService)
        {
            _productService = productService;
        }

        [HttpGet("stock/pdf")]
        public async Task<IActionResult> DownloadStockReportPdf()
        {
            var products = await _productService.GetAll();
            var htmlContent = GenerateStockReportHtml(products);

            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlContent);
            doc.Save(typeof(Products).Name + "test.pdf");
            doc.Close();

            return Ok(products);
        }

        private string GenerateStockReportHtml(IEnumerable<ProductModel> products)
        {
            var html = "<html><body><h1>Stock Report</h1><table border='1'><tr><th>Product</th><th>Stock Level</th></tr>";

            foreach (var product in products)
            {
                html += $"<tr><td>{product.Name}</td><td>{product.StockQuantity}</td></tr>";
            }

            html += "</table></body></html>";
            return html;
        }
    }
}