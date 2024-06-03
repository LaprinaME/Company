using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Company.Controllers;
using Company.DataContext;
using Company.Models;

namespace Company.Test
{
    public class AccessRightsControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAccessRights()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Seed the in-memory database with test data
            using (var context = new CompanyContext(options))
            {
                context.Права_доступа.Add(new Права_доступа { Код_права = 1, Название_права = "Read", Код_роли = 1 });
                context.Права_доступа.Add(new Права_доступа { Код_права = 2, Название_права = "Write", Код_роли = 2 });
                context.Роли.Add(new Роли { Код_роли = 1, Название_роли = "Admin" });
                context.Роли.Add(new Роли { Код_роли = 2, Название_роли = "User" });
                context.SaveChanges();
            }

            // Use a new context instance for the test
            using (var context = new CompanyContext(options))
            {
                var controller = new AccessRightsController(context);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Права_доступа>>(viewResult.ViewData.Model);
                Assert.Equal(2, model.Count());
            }
        }
    }
}
