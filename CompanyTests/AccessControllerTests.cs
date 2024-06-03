using Xunit;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Company.Controllers;
using Company.DataContext;
using Company.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Company.Test
{
    public class AccessControllerTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfAccessRestrictions()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Seed the in-memory database with test data
            using (var context = new CompanyContext(options))
            {
                context.Ограничения_доступа.Add(new Ограничения_доступа { Код_ограничения = 1, Код_сотрудника = 1, Код_расписания = 1 });
                context.Ограничения_доступа.Add(new Ограничения_доступа { Код_ограничения = 2, Код_сотрудника = 2, Код_расписания = 2 });
                context.SaveChanges();
            }

            // Use a new context instance for the test
            using (var context = new CompanyContext(options))
            {
                var controller = new AccessController(context);

                // Act
                var result = await controller.Index();

                // Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<Ограничения_доступа>>(viewResult.ViewData.Model);
                Assert.Equal(2, model.Count());
            }
        }
    }
}
