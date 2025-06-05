using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System_Rezerwacji_Biletów.Service;
using System_Rezerwacji_Biletów.ViewModels;
using FluentValidation.Results;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace System_Rezerwacji_Biletów.Controllers
{
    [Authorize(Policy = "OnlyAdmin")]
    public class AdminController : Controller
    {
        private readonly IValidator<UserViewModel> _userValidator;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IValidator<UserViewModel> validator,
         IMapper mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userValidator = validator;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Dashboard()
        {

            var users = _userManager.Users.ToList();

            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            var userViewModel = new List<UserViewModel>();

            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                userViewModel.Add(new UserViewModel
                {
                    UserID = user.Id,
                    Name = user.UserName,
                    BirthDate = DateTime.MinValue,
                    email = user.Email,
                    CurrentRole = roles.FirstOrDefault() ?? "NoRole",
                    AvailableRoles = allRoles
                });
            }

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserRole(string id, string CurrentRole)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(CurrentRole))
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
            var addResult = await _userManager.AddToRoleAsync(user, CurrentRole);
            if (!removeResult.Succeeded || !addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove existing roles");
                return RedirectToAction("ListUser");
            }


            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = await _userManager.FindByIdAsync(id);

            if (userId == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(userId);

            return RedirectToAction(nameof(Dashboard));
        }
        
        
    }
}
