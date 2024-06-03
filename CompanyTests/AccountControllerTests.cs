using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Company.Controllers;
using Company.Models;
using Company.ViewModels;
using Company.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Tests
{
    public class AccountControllerTests
    {
        private Mock<CompanyContext> _mockContext;
        private AccountController _controller;

        public AccountControllerTests()
        {
            _mockContext = new Mock<CompanyContext>();
            _controller = new AccountController(_mockContext.Object);
        }

        [Fact]
        public async Task Login_Post_Returns_RedirectToActionResult_When_User_Is_Valid_And_Has_Role()
        {
            // Arrange
            var model = new LoginViewModel { Login = "testUser", Password = "testPassword" };
            var role = new Роли { Код_роли = 1 };
            var user = new Аккаунты { Логин = "testUser", Пароль = "testPassword", Роли = role };

            var mockSet = new Mock<DbSet<Аккаунты>>();
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.Provider).Returns(new List<Аккаунты> { user }.AsQueryable().Provider);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.Expression).Returns(new List<Аккаунты> { user }.AsQueryable().Expression);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.ElementType).Returns(new List<Аккаунты> { user }.AsQueryable().ElementType);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.GetEnumerator()).Returns(new List<Аккаунты> { user }.AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Аккаунты).Returns(mockSet.Object);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Menu", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Login_Post_Returns_ViewResult_With_ModelStateError_When_User_Is_Invalid()
        {
            // Arrange
            var model = new LoginViewModel { Login = "invalidUser", Password = "invalidPassword" };

            var mockSet = new Mock<DbSet<Аккаунты>>();
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.Provider).Returns(new List<Аккаунты>().AsQueryable().Provider);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.Expression).Returns(new List<Аккаунты>().AsQueryable().Expression);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.ElementType).Returns(new List<Аккаунты>().AsQueryable().ElementType);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.GetEnumerator()).Returns(new List<Аккаунты>().AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Аккаунты).Returns(mockSet.Object);

            // Act
            var result = await _controller.Login(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedModel = Assert.IsAssignableFrom<LoginViewModel>(viewResult.ViewData.Model);
            Assert.Equal(model, returnedModel);
            Assert.True(_controller.ModelState.ContainsKey(string.Empty));
            Assert.Equal("Неверный логин или пароль", _controller.ModelState[string.Empty].Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Register_Post_Returns_RedirectToActionResult_When_ModelState_Is_Valid()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Login = "newUser",
                Password = "password",
                ConfirmPassword = "password",
                RoleCode = 1,
                AccountCode = 1
            };

            var role = new Роли { Код_роли = 1 };
            var mockRoleSet = new Mock<DbSet<Роли>>();
            mockRoleSet.Setup(m => m.FindAsync(model.RoleCode)).ReturnsAsync(role);

            var mockAccountSet = new Mock<DbSet<Аккаунты>>();
            mockAccountSet.As<IQueryable<Аккаунты>>().Setup(m => m.Provider).Returns(new List<Аккаунты>().AsQueryable().Provider);
            mockAccountSet.As<IQueryable<Аккаунты>>().Setup(m => m.Expression).Returns(new List<Аккаунты>().AsQueryable().Expression);
            mockAccountSet.As<IQueryable<Аккаунты>>().Setup(m => m.ElementType).Returns(new List<Аккаунты>().AsQueryable().ElementType);
            mockAccountSet.As<IQueryable<Аккаунты>>().Setup(m => m.GetEnumerator()).Returns(new List<Аккаунты>().AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Аккаунты).Returns(mockAccountSet.Object);
            _mockContext.Setup(c => c.Роли).Returns(mockRoleSet.Object);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
        }

        [Fact]
        public async Task Register_Post_Returns_ViewResult_With_ModelStateError_When_Login_Is_Not_Unique()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Login = "existingUser",
                Password = "password",
                ConfirmPassword = "password",
                RoleCode = 1,
                AccountCode = 1
            };

            var existingUser = new Аккаунты { Логин = "existingUser" };

            var mockSet = new Mock<DbSet<Аккаунты>>();
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.Provider).Returns(new List<Аккаунты> { existingUser }.AsQueryable().Provider);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.Expression).Returns(new List<Аккаунты> { existingUser }.AsQueryable().Expression);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.ElementType).Returns(new List<Аккаунты> { existingUser }.AsQueryable().ElementType);
            mockSet.As<IQueryable<Аккаунты>>().Setup(m => m.GetEnumerator()).Returns(new List<Аккаунты> { existingUser }.AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Аккаунты).Returns(mockSet.Object);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedModel = Assert.IsAssignableFrom<RegisterViewModel>(viewResult.ViewData.Model);
            Assert.Equal(model, returnedModel);
            Assert.True(_controller.ModelState.ContainsKey("Login"));
            Assert.Equal("Пользователь с таким логином уже существует", _controller.ModelState["Login"].Errors.First().ErrorMessage);
        }

        [Fact]
        public async Task Register_Post_Returns_ViewResult_With_ModelStateError_When_Role_Is_Not_Valid()
        {
            // Arrange
            var model = new RegisterViewModel
            {
                Login = "newUser",
                Password = "password",
                ConfirmPassword = "password",
                RoleCode = 999, // Invalid role code
                AccountCode = 1
            };

            var mockRoleSet = new Mock<DbSet<Роли>>();
            mockRoleSet.As<IQueryable<Роли>>().Setup(m => m.Provider).Returns(new List<Роли>().AsQueryable().Provider);
            mockRoleSet.As<IQueryable<Роли>>().Setup(m => m.Expression).Returns(new List<Роли>().AsQueryable().Expression);
            mockRoleSet.As<IQueryable<Роли>>().Setup(m => m.ElementType).Returns(new List<Роли>().AsQueryable().ElementType);
            mockRoleSet.As<IQueryable<Роли>>().Setup(m => m.GetEnumerator()).Returns(new List<Роли>().AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Роли).Returns(mockRoleSet.Object);

            // Act
            var result = await _controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedModel = Assert.IsAssignableFrom<RegisterViewModel>(viewResult.ViewData.Model);
            Assert.Equal(model, returnedModel);
            Assert.True(_controller.ModelState.ContainsKey("RoleCode"));
            Assert.Equal("Роль с указанным кодом не существует", _controller.ModelState["RoleCode"].Errors.First().ErrorMessage);
        }
    }
}
