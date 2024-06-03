using Company.Controllers;
using Company.DataContext;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Company.Tests
{
    public class RoleControllerTests
    {
        private readonly RoleController _controller;
        private readonly CompanyContext _context;

        public RoleControllerTests()
        {
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new CompanyContext(options);

            SeedDatabase();

            _controller = new RoleController(_context);
        }

        private void SeedDatabase()
        {
            var roles = new[]
            {
                new Роли { Код_роли = 1, Название_роли = "Role 1" },
                new Роли { Код_роли = 2, Название_роли = "Role 2" },
                new Роли { Код_роли = 3, Название_роли = "Role 3" }
            };

            _context.Роли.AddRange(roles);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_Returns_ViewResult_With_Roles()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Роли>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Index_Returns_Correct_Roles()
        {
            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Роли>>(viewResult.ViewData.Model);
            Assert.Equal("Role 1", model.First().Название_роли);
        }
    }
}
