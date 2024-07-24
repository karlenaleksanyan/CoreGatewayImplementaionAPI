using CoreGateway.Abstraction;
using CoreGateway.Models.Models;
using CoreGatewayAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoreGatewayUnitTests
{
    public class ProductControllerTests
    {
        private readonly Mock<IService<ProductModel>> _mockProductService;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _mockProductService = new Mock<IService<ProductModel>>();
            _controller = new ProductController(_mockProductService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsProducts()
        {
            // Arrange
            var products = new List<ProductModel> { new ProductModel { Id = Guid.NewGuid(), Name = "Test Product" } };
            _mockProductService.Setup(s => s.GetAll()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<ProductModel>>(okResult.Value);
            Assert.Equal(products, returnValue);
        }

        [Fact]
        public async Task Get_ReturnsProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new ProductModel { Id = productId, Name = "Test Product" };
            _mockProductService.Setup(s => s.GetFirstOrDefault(It.IsAny<Guid>())).ReturnsAsync(product);

            // Act
            var result = await _controller.Get(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<ProductModel>(okResult.Value);
            Assert.Equal(product, returnValue);
        }

        [Fact]
        public async Task Create_AddsProduct()
        {
            // Arrange
            var product = new ProductModel { Id = Guid.NewGuid(), Name = "Test Product" };
            _mockProductService.Setup(s => s.Add(product)).Returns(Task.CompletedTask);

            // Act
            await _controller.Create(product);

            // Assert
            _mockProductService.Verify(s => s.Add(product), Times.Once);
        }

        [Fact]
        public async Task Update_UpdatesProduct()
        {
            // Arrange
            var product = new ProductModel { Id = Guid.NewGuid(), Name = "Updated Product" };
            _mockProductService.Setup(s => s.GetFirstOrDefault(product.Id)).ReturnsAsync(product);
            _mockProductService.Setup(s => s.Update(product)).Returns(Task.CompletedTask);

            // Act
            await _controller.Update(product);

            // Assert
            _mockProductService.Verify(s => s.Update(product), Times.Once);
        }

        [Fact]
        public async Task Delete_DeletesProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new ProductModel { Id = productId, Name = "Test Product" };
            _mockProductService.Setup(s => s.GetFirstOrDefault(productId)).ReturnsAsync(product);
            _mockProductService.Setup(s => s.Delete(productId)).Returns(Task.CompletedTask);

            // Act
            await _controller.Delete(productId);

            // Assert
            _mockProductService.Verify(s => s.Delete(productId), Times.Once);
        }
    }
}