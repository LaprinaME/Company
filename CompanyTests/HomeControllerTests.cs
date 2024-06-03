using Company.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Company.Test
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult);
        }
    }
}
