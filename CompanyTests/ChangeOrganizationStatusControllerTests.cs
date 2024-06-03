using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Company.Controllers;
using Company.Models;
using Company.ViewModels;
using Company.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Company.Tests
{
    public class ChangeOrganizationStatusControllerTests
    {
        private readonly ChangeOrganizationStatusController _controller;
        private readonly CompanyContext _context;

        public ChangeOrganizationStatusControllerTests()
        {
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CompanyContext(options);
            _controller = new ChangeOrganizationStatusController(_context);
        }

        [Fact]
        public async Task ChangeOrganizationStatus_Post_Returns_RedirectToActionResult_When_ModelState_Is_Valid()
        {
            // Arrange
            var viewModel = new ChangeOrganizationStatusViewModel
            {
                Код_организации = 1,
                Название_организации = "Test Organization",
                Код_статуса = 2,
                Код_родительской_организации = 3
            };

            // Act
            var result = await _controller.ChangeOrganizationStatus(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ChangeOrganizationStatusController.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task ChangeOrganizationStatus_Post_Returns_ViewResult_With_Model_When_ModelState_Is_Invalid()
        {
            // Arrange
            var viewModel = new ChangeOrganizationStatusViewModel
            {
                Код_организации = 1,
                Название_организации = "Test Organization",
                Код_статуса = 2,
                Код_родительской_организации = 3
            };
            _controller.ModelState.AddModelError("Код_статуса", "Required");

            // Act
            var result = await _controller.ChangeOrganizationStatus(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedModel = Assert.IsAssignableFrom<ChangeOrganizationStatusViewModel>(viewResult.ViewData.Model);
            Assert.Equal(viewModel, returnedModel);
        }
    }
}
