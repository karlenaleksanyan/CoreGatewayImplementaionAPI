using CoreGateway.Abstraction;
using CoreGateway.Models.Models;
using CoreGatewayAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CoreGatewayUnitTests
{
    public class CategoryControllerTests
    {
        private readonly Mock<IService<CategoryModel>> _mockCategoryService;
        private readonly CategoryController _controller;

        public CategoryControllerTests()
        {
            _mockCategoryService = new Mock<IService<CategoryModel>>();
            _controller = new CategoryController(_mockCategoryService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsCategories()
        {
            // Arrange
            var categories = new List<CategoryModel> { new CategoryModel { Id = Guid.NewGuid(), Name = "Test Category" } };
            _mockCategoryService.Setup(s => s.GetAll()).ReturnsAsync(categories);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<CategoryModel>>(okResult.Value);
            Assert.Equal(categories, returnValue);
        }

        [Fact]
        public async Task Get_ReturnsCategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = new CategoryModel { Id = categoryId, Name = "Test Category" };
            _mockCategoryService.Setup(s => s.GetFirstOrDefault(categoryId)).ReturnsAsync(category);

            // Act
            var result = await _controller.Get(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<CategoryModel>(okResult.Value);
            Assert.Equal(category, returnValue);
        }

        [Fact]
        public async Task Create_AddsCategory()
        {
            // Arrange
            var category = new CategoryModel { Id = Guid.NewGuid(), Name = "Test Category" };
            _mockCategoryService.Setup(s => s.Add(category)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(category);

            // Assert
            _mockCategoryService.Verify(s => s.Add(category), Times.Once);
        }

        [Fact]
        public async Task Update_UpdatesCategory()
        {
            // Arrange
            var category = new CategoryModel { Id = Guid.NewGuid(), Name = "Updated Category" };
            _mockCategoryService.Setup(s => s.GetFirstOrDefault(category.Id)).ReturnsAsync(category);
            _mockCategoryService.Setup(s => s.Update(category)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(category);

            // Assert
            _mockCategoryService.Verify(s => s.Update(category), Times.Once);
        }

        [Fact]
        public async Task Delete_DeletesCategory()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _mockCategoryService.Setup(s => s.Delete(categoryId)).Returns(Task.CompletedTask);

            var category = new CategoryModel { Id = categoryId, Name = "Test Category" };
            _mockCategoryService.Setup(s => s.GetFirstOrDefault(categoryId)).ReturnsAsync(category);
            _mockCategoryService.Setup(s => s.Delete(categoryId)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(categoryId);

            // Assert
            _mockCategoryService.Verify(s => s.Delete(categoryId), Times.Once);
        }
    }
}