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
    public class AddEmployeesControllerTests
    {
        private Mock<CompanyContext> _mockContext;
        private AddEmployeesController _controller;

        public AddEmployeesControllerTests()
        {
            _mockContext = new Mock<CompanyContext>();
            _controller = new AddEmployeesController(_mockContext.Object);
        }

        [Fact]
        public async Task AddEmployees_Post_Returns_RedirectToActionResult_When_ModelState_Is_Valid()
        {
            // Arrange
            var model = new AddEmployeesViewModel
            {
                ФИО = "Иванов Иван Иванович",
                Пол = "М",
                Дата_рождения = new System.DateTime(1990, 1, 1),
                СНИЛС = "123-456-789 00",
                Мобильный_телефон = "+79991234567",
                Адрес_электронной_почты = "ivanov@example.com",
                Адрес_проживания = "ул. Пушкина, д. 10",
                Должность = "Менеджер",
                Код_аккаунта = 1,
                Код_статуса = 1,
                Код_сотрудника = 1
            };

            var mockSet = new Mock<DbSet<Сотрудники>>();
            _mockContext.Setup(c => c.Сотрудники).Returns(mockSet.Object);

            // Act
            var result = await _controller.AddEmployees(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            _mockContext.Verify(m => m.Add(It.IsAny<Сотрудники>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<System.Threading.CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddEmployees_Post_Returns_ViewResult_With_Model_When_ModelState_Is_Invalid()
        {
            // Arrange
            var model = new AddEmployeesViewModel
            {
                ФИО = "Иванов Иван Иванович",
                Пол = "М",
                Дата_рождения = new System.DateTime(1990, 1, 1),
                СНИЛС = "123-456-789 00",
                Мобильный_телефон = "+79991234567",
                Адрес_электронной_почты = "ivanov@example.com",
                Адрес_проживания = "ул. Пушкина, д. 10",
                Должность = "Менеджер",
                Код_аккаунта = 1,
                Код_статуса = 1,
                Код_сотрудника = 1
            };

            _controller.ModelState.AddModelError("ФИО", "Введите ФИО");

            // Act
            var result = await _controller.AddEmployees(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedModel = Assert.IsAssignableFrom<AddEmployeesViewModel>(viewResult.ViewData.Model);
            Assert.Equal(model, returnedModel);
        }
    }
}
