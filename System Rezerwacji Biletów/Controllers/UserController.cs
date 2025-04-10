using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Repository;
using System_Rezerwacji_Biletów.Service;
using System_Rezerwacji_Biletów.ViewModels;
using FluentValidation;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Diagnostics;
using NuGet.Protocol;

namespace System_Rezerwacji_Biletów.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserViewModel> _userValidator;

        public UserController(IUserService userService, IValidator<UserViewModel> validator)
        {
            _userService = userService;
            _userValidator = validator;
        }

        public IActionResult ListUser()
        {
            var users = _userService.GetAllUsers();

            var userViewModel = users.Select(user => new UserViewModel
            {
                UserID = user.UserID,
                Name = user.Name,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                email = user.email
            });
            
            return View(userViewModel);
        }

        public IActionResult Create()
        {     
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserViewModel userModel)
        {
            ValidationResult userValidation = _userValidator.Validate(userModel);

            if (!userValidation.IsValid)
            {
                foreach (var errors in userValidation.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }
                return View(userModel);
            } 
            else {
                var user = new User
                {
                    UserID = userModel.UserID,
                    Name = userModel.Name,
                    LastName = userModel.LastName,
                    BirthDate = userModel.BirthDate,
                    email = userModel.email
                };

                _userService.CreateUser(user);

                return RedirectToAction(nameof(ListUser));
            }
        }


        public IActionResult Update(int id)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }
            var userModel = new UserViewModel
            {
                UserID = user.UserID,
                Name = user.Name,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                email = user.email
            };
            return View(userModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, UserViewModel userModel)
        {
            ValidationResult userValidation = _userValidator.Validate(userModel);

            if (!userValidation.IsValid)
            {
                foreach(var errors in userValidation.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }
                return View(userModel);
            }
            else
            {
                var user = new User
                {
                    UserID = userModel.UserID,
                    Name = userModel.Name,
                    LastName = userModel.LastName,
                    BirthDate = userModel.BirthDate,
                    email = userModel.email
                };

                await _userService.UpdateUser(id, user);
                return RedirectToAction(nameof(ListUser));
            }
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            var userId = _userService.GetUserById(id);

            if(userId == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(id);

            return RedirectToAction(nameof(ListUser));
        }
    }
}
