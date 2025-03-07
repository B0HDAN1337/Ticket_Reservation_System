using Microsoft.AspNetCore.Mvc;
using System_Rezerwacji_Biletów.Data;
using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Controllers
{
    public class UserController : Controller
    {
        private readonly SystemReservationContext _context;

        public UserController(SystemReservationContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID, Name, LastName, BirthDate, email")] User user)
        {
            if(ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return View(nameof(Index));
            }
            return View(user);
        }
    }
}
