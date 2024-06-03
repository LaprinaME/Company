using Company.Controllers;
using Company.DataContext;
using Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Company.Tests
{
    public class OrganizationsControllerTests
    {
        private readonly OrganizationsController _controller;
        private readonly CompanyContext _context;

        public OrganizationsControllerTests()
        {
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new CompanyContext(options);

            SeedDatabase();

            _controller = new OrganizationsController(_context);
        }

        private void SeedDatabase()
        {
            var organizations = new[]
            {
                new Организации { Код_организации = 1, Название_организации = "Org 1", Дата_закрытия = DateTime.Now },
                new Организации { Код_организации = 2, Название_организации = "Org 2", Дата_закрытия = DateTime.Now.AddDays(-1) },
                new Организации { Код_организации = 3, Название_организации = "Org 3", Дата_закрытия = DateTime.Now.AddDays(-2) }
            };

            _context.Организации.AddRange(organizations);
            _context.SaveChanges();
        }

        [Fact]
        public async Task Index_Returns_ViewResult_With_Organizations()
        {
            // Act
            var result = await _controller.Index(null, null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IQueryable<Организации>>(viewResult.ViewData.Model);
            Assert.Equal(3, model.Count());
        }

        [Fact]
        public async Task Index_Returns_Organizations_Filtered_By_SearchString()
        {
            // Act
            var result = await _controller.Index("Org 1", null);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IQueryable<Организации>>(viewResult.ViewData.Model);
            Assert.Single(model);
            Assert.Equal("Org 1", model.First().Название_организации);
        }

        [Fact]
        public async Task Index_Returns_Organizations_Sorted_By_Name_Descending()
        {
            // Act
            var result = await _controller.Index(null, "name_desc");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IQueryable<Организации>>(viewResult.ViewData.Model);
            Assert.Equal("Org 3", model.First().Название_организации);
        }

        [Fact]
        public async Task Index_Returns_Organizations_Sorted_By_Date_Descending()
        {
            // Act
            var result = await _controller.Index(null, "date_desc");

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IQueryable<Организации>>(viewResult.ViewData.Model);
            Assert.Equal("Org 1", model.First().Название_организации);
        }
    }
}
