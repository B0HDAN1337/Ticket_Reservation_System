using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
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


        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("UserID, Name, LastName, BirthDate, email")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ListUser));
            } 

                return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await _context.Users.FindAsync(id);

            if(userId == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userId);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ListUser));
        }
    }
}
