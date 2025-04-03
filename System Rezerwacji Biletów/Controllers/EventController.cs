﻿using Microsoft.AspNetCore.Mvc;
using System_Rezerwacji_Biletów.Repository;
using System.Threading.Tasks;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Service;

namespace System_Rezerwacji_Biletów.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult ListEvent()
        {
            var event_ = _eventService.GetAllEvents();
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
                _eventService.CreateEvent(event_);
                return RedirectToAction(nameof(ListEvent));
            }

            return View(event_);
        }


        public  IActionResult Update(int id)
        {
            var event_ = _eventService.GetEventById(id);

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
                await _eventService.UpdateEvent(id, event_);
                return RedirectToAction(nameof(ListEvent));
            }
            return View(event_);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            var event_ = _eventService.GetEventById(id);
            if (event_ == null)
            {
                return NotFound();
            }

            _eventService.DeleteEvent(id);

            return RedirectToAction(nameof(ListEvent));
        }



    }
}
