using System.Collections.Generic;
using System_Rezerwacji_Biletów.Controllers;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Service;
using System_Rezerwacji_Biletów.ViewModels;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace System_Rezerwacji_Biletów.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IValidator<UserViewModel>> _mockValidator;
        private readonly Mock<IMapper> _mockMapper;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockValidator = new Mock<IValidator<UserViewModel>>();
            _mockMapper = new Mock<IMapper>();

            _controller = new UserController(_mockUserService.Object, _mockValidator.Object, _mockMapper.Object);
        }

        [Fact]
        public void ListUser_ReturnsViewResult_WithListOfUserViewModels()
        {
            var users = new List<User>
            {
                new User { UserID = 1, Name = "Jan", LastName = "Kowalski", email = "jan@example.com" }
            }.AsQueryable();

            _mockUserService.Setup(s => s.GetAllUsers()).Returns(users);

            var userViewModels = new List<UserViewModel>
            {
                new UserViewModel { UserID = "1", Name = "Jan", LastName = "Kowalski", email = "jan@example.com" }
            };
            _mockMapper.Setup(m => m.Map<List<UserViewModel>>(users)).Returns(userViewModels);

            var result = _controller.ListUser();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<UserViewModel>>(viewResult.Model);
            Assert.Single(model);
        }


        [Fact]
        public void Create_Post_ReturnsViewWithModel_WhenValidationFails()
        {
            var userModel = new UserViewModel { Name = "", LastName = "", email = "bademail" };

            _mockValidator.Setup(v => v.Validate(userModel))
                          .Returns(new ValidationResult(new List<ValidationFailure> {
                              new ValidationFailure("Email", "Invalid email address")
                          }));

            var result = _controller.Create(userModel);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.False(_controller.ModelState.IsValid);
            Assert.Equal(userModel, viewResult.Model);
        }
    }
}
