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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace System_Rezerwacji_Biletów.Controllers
{
    [Authorize(Policy = "OnlyAdmin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserViewModel> _userValidator;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IValidator<UserViewModel> validator, IMapper mapper)
        {
            _userService = userService;
            _userValidator = validator;
            _mapper = mapper;
        }

        public IActionResult ListUser()
        {

            var users = _userService.GetAllUsers();
            var viewModel = _mapper.Map<List<UserViewModel>>(users);
            
            return View(viewModel);
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

                var viewModel = _mapper.Map<User>(userModel);

                _userService.CreateUser(viewModel);

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

            var userModel = _mapper.Map<UserViewModel>(user);

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
                var user = _mapper.Map<User>(userModel);

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
