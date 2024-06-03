using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Company.Controllers;
using Company.Models;
using Company.DataContext;
using Company.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Company.Tests
{
    public class AddOrganizationControllerTests
    {
        private Mock<CompanyContext> _mockContext;
        private AddOrganizationController _controller;

        public AddOrganizationControllerTests()
        {
            // Setting up mock context
            _mockContext = new Mock<CompanyContext>();

            // Initializing the controller with the mock context
            _controller = new AddOrganizationController(_mockContext.Object);
        }

        [Fact]
        public void Index_Returns_ViewResult_With_ViewModel()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AddOrganizationViewModel>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task AddOrganization_Post_Returns_RedirectToActionResult_When_ModelState_Is_Valid()
        {
            // Arrange
            var viewModel = new AddOrganizationViewModel
            {
                Название_организации = "Test Organization",
                ИНН = "1234567890",
                КПП = "0987654321",
                ОГРН = "1122334455667",
                Код = 1,
                Тип_организации = "ООО",
                Дата_закрытия = DateTime.Now,
                Код_родительской_организации = 0,
                Код_организации = 1,
                Код_статуса = 1
            };

            // Act
            var result = await _controller.AddOrganization(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task AddOrganization_Post_Returns_ViewResult_When_ModelState_Is_Invalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("ИНН", "Required");

            var viewModel = new AddOrganizationViewModel
            {
                Название_организации = "Test Organization",
                // ИНН is missing to trigger the model state error
                КПП = "0987654321",
                ОГРН = "1122334455667",
                Код = 1,
                Тип_организации = "ООО",
                Дата_закрытия = DateTime.Now,
                Код_родительской_организации = 0,
                Код_организации = 1,
                Код_статуса = 1
            };

            // Act
            var result = await _controller.AddOrganization(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<AddOrganizationViewModel>(viewResult.ViewData.Model);
            Assert.Equal(viewModel, model);
        }
    }
}
