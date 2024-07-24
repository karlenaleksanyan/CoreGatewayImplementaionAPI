using CoreGateway.Abstraction;
using CoreGateway.Models.Models;
using CoreGatewayAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoreGatewayUnitTests
{
    public class SupplierControllerTests
    {
        private readonly Mock<IService<SupplierModel>> _mockSupplierService;
        private readonly SupplierController _controller;

        public SupplierControllerTests()
        {
            _mockSupplierService = new Mock<IService<SupplierModel>>();
            _controller = new SupplierController(_mockSupplierService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsSuppliers()
        {
            // Arrange
            var suppliers = new List<SupplierModel> { new SupplierModel { Id = Guid.NewGuid(), Name = "Test Supplier" } };
            _mockSupplierService.Setup(s => s.GetAll()).ReturnsAsync(suppliers);

            // Act
            ActionResult<IEnumerable<SupplierModel>> result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<SupplierModel>>(okResult.Value);
            Assert.Equal(suppliers, returnValue);
        }

        [Fact]
        public async Task Get_ReturnsSupplier()
        {
            // Arrange
            var supplierId = Guid.NewGuid();
            var supplier = new SupplierModel { Id = supplierId, Name = "Test Supplier" };
            _mockSupplierService.Setup(s => s.GetFirstOrDefault(supplierId)).ReturnsAsync(supplier);

            // Act
            var result = await _controller.Get(supplierId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<SupplierModel>(okResult.Value);
            Assert.Equal(supplier, returnValue);
        }

        [Fact]
        public async Task Create_AddsSupplier()
        {
            // Arrange
            var supplier = new SupplierModel { Id = Guid.NewGuid(), Name = "Test Supplier" };
            _mockSupplierService.Setup(s => s.Add(supplier)).Returns(Task.CompletedTask);

            // Act
            await _controller.Create(supplier);

            // Assert
            _mockSupplierService.Verify(s => s.Add(supplier), Times.Once);
        }

        [Fact]
        public async Task Update_UpdatesSupplier()
        {
            // Arrange
            var supplier = new SupplierModel { Id = Guid.NewGuid(), Name = "Updated Supplier" };
            _mockSupplierService.Setup(s => s.GetFirstOrDefault(supplier.Id)).ReturnsAsync(supplier);
            _mockSupplierService.Setup(s => s.Update(supplier)).Returns(Task.CompletedTask);

            // Act
            await _controller.Update(supplier);

            // Assert
            _mockSupplierService.Verify(s => s.Update(supplier), Times.Once);
        }

        [Fact]
        public async Task Delete_DeletesSupplier()
        {
            // Arrange
            var supplierId = Guid.NewGuid();
            var supplier = new SupplierModel { Id = supplierId, Name = "Test Supplier" };
            _mockSupplierService.Setup(s => s.GetFirstOrDefault(supplierId)).ReturnsAsync(supplier);
            _mockSupplierService.Setup(s => s.Delete(supplierId)).Returns(Task.CompletedTask);

            // Act
            await _controller.Delete(supplierId);

            // Assert
            _mockSupplierService.Verify(s => s.Delete(supplierId), Times.Once);
        }
    }
}