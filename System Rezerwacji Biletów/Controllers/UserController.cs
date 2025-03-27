using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System_Rezerwacji_Biletów.Data;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Repository;

namespace System_Rezerwacji_Biletów.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult ListUser()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.Create(user);
            
                return RedirectToAction(nameof(ListUser));
            }
            return View(user);
        }


        public IActionResult Update(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (ModelState.IsValid)
            {
                await _userRepository.Update(id, user);
                return RedirectToAction(nameof(ListUser));
            } 
                return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            var userId =  _userRepository.GetById(id);

            if(userId == null)
            {
                return NotFound();
            }

            _userRepository.Delete(id);

            return RedirectToAction(nameof(ListUser));
        }
    }
}
