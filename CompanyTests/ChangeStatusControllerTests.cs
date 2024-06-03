using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Company.Controllers;
using Company.Models;
using Company.ViewModels;
using Company.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Company.Tests
{
    public class ChangeStatusControllerTests
    {
        private readonly ChangeStatusController _controller;
        private readonly CompanyContext _context;

        public ChangeStatusControllerTests()
        {
            var options = new DbContextOptionsBuilder<CompanyContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new CompanyContext(options);

            // Seed the database with test data
            SeedDatabase();

            _controller = new ChangeStatusController(_context);
        }

        private void SeedDatabase()
        {
            var employees = new List<Сотрудники>
            {
                new Сотрудники { Код_сотрудника = 1, ФИО = "Иван Иванов", Код_статуса = 1 },
                new Сотрудники { Код_сотрудника = 2, ФИО = "Петр Петров", Код_статуса = 2 }
            };

            var statuses = new List<Статусы>
            {
                new Статусы { Код_статуса = 1, Название_статуса = "Активный" },
                new Статусы { Код_статуса = 2, Название_статуса = "Неактивный" }
            };

            _context.Сотрудники.AddRange(employees);
            _context.Статусы.AddRange(statuses);
            _context.SaveChanges();
        }

        [Fact]
        public void Index_Returns_ViewResult_With_ChangeStatusViewModel()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ChangeStatusViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Employees.Count);
            Assert.Equal(2, model.Statuses.Count);
        }

        [Fact]
        public async Task ChangeStatus_Post_Returns_RedirectToActionResult_When_ModelState_Is_Valid()
        {
            // Arrange
            var viewModel = new ChangeStatusViewModel
            {
                EmployeeId = 1,
                StatusId = 2
            };

            // Act
            var result = await _controller.ChangeStatus(viewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(ChangeStatusController.Index), redirectToActionResult.ActionName);

            // Verify the status change
            var employee = await _context.Сотрудники.FindAsync(1);
            Assert.Equal(2, employee.Код_статуса);
        }

        [Fact]
        public async Task ChangeStatus_Post_Returns_ViewResult_With_Model_When_ModelState_Is_Invalid()
        {
            // Arrange
            var viewModel = new ChangeStatusViewModel
            {
                EmployeeId = 1,
                StatusId = 2
            };
            _controller.ModelState.AddModelError("StatusId", "Required");

            // Act
            var result = await _controller.ChangeStatus(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ChangeStatusViewModel>(viewResult.ViewData.Model);
            Assert.Equal(viewModel, model);
        }

        [Fact]
        public async Task ChangeStatus_Post_Returns_ViewResult_When_Employee_Not_Found()
        {
            // Arrange
            var viewModel = new ChangeStatusViewModel
            {
                EmployeeId = 999, // Non-existent employee
                StatusId = 1
            };

            // Act
            var result = await _controller.ChangeStatus(viewModel);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ChangeStatusViewModel>(viewResult.ViewData.Model);
            Assert.Equal(viewModel, model);
        }
    }
}
