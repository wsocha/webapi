using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebAPI.Controllers;
using WebAPI.CustomExceptions;
using WebAPI.Extensibility;
using WebAPI.Models.DTOs;
using WebAPI.Models.Entities;

namespace WebAPI.Tests
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task DeleteProduct_ReturnNotFound_IfProductDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var mockLogger = new Mock<ILogger<ProductsController>>();
            //mockLogger.Setup(logger => logger.LogInformation(message)).;
            IEnumerable<ProductDto> products = new List<ProductDto>() { };
            mockRepo.Setup(repo => repo.DeleteProduct(It.IsAny<int>()))
                .Throws(new NotFoundException(""));
            var controller = new ProductsController(mockLogger.Object, mockRepo.Object);

            // Act
            var result = await controller.DeleteProduct(0);

            // Assert
            var notFound = Assert.IsType<NotFoundResult>(result);
        }
    }
}