using Microsoft.AspNetCore.Mvc;
using System_Rezerwacji_Biletów.Repository;
using System.Threading.Tasks;
using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository;

        public EventController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IActionResult ListEvent()
        {
            var event_ =  _eventRepository.GetAll();
            return View(event_);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event event_)
        {
            if (ModelState.IsValid)
            {
                 _eventRepository.Create(event_);
                return RedirectToAction(nameof(ListEvent));
            }

            return View(event_);
        }


        public  IActionResult Update(int id)
        {
            var event_ =  _eventRepository.GetById(id);

            if (event_ == null)
            {
                return NotFound();
            }
            return View(event_);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Event event_)
        {

            if (ModelState.IsValid)
            {
                await _eventRepository.Update(id, event_);
                return RedirectToAction(nameof(ListEvent));
            }
            return View(event_);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var event_ =  _eventRepository.GetById(id);
            if (event_ == null)
            {
                return NotFound();
            }

             _eventRepository.Delete(id);

            return RedirectToAction(nameof(ListEvent));
        }



    }
}
