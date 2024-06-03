using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Company.Controllers;
using Company.DataContext;
using Company.Models;

namespace Company.Test
{
    public class ApplicationsControllerTests
    {
        [Fact]
        public async Task Create_ValidApplication_RedirectsToIndex()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new CompanyContext(options))
            {
                var controller = new ApplicationsController(context);
                var application = new Приложения
                {
                    Название_приложения = "Test Application",
                    Тип_приложения = "Test Type"
                };

                // Act
                var result = await controller.Create(application) as RedirectToActionResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Index", result.ActionName);
            }
        }
    }
}
