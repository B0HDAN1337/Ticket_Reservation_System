using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System_Rezerwacji_Biletów.Data;
using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Controllers
{
    public class UserController : Controller
    {
        private readonly SystemReservationContext _context;

        public UserController(SystemReservationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> ListUser()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        public IActionResult Edit()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID, Name, LastName, BirthDate, email")] User user)
        {
            
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            
                return RedirectToAction(nameof(ListUser));
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.UserID)
            {
                return NotFound();  
            }

            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ListUser));
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return View(user);
            }
                return View(user);
        }
    }
}
