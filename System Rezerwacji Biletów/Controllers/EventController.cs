﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Service;
using System_Rezerwacji_Biletów.ViewModels;


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

            var eventViewModel = event_.Select(events => new EventViewModel
            {
                EventID = events.EventID,
                NameEvent = events.NameEvent,
                Date = events.Date,
                Location = events.Location,
                description = events.description
            });

            return View(eventViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EventViewModel eventModel)
        {
            if (ModelState.IsValid)
            {
                var events = new Event
                {
                    EventID = eventModel.EventID,
                    NameEvent = eventModel.NameEvent,
                    Date = eventModel.Date,
                    Location = eventModel.Location,
                    description = eventModel.description
                };

                _eventService.CreateEvent(events);
                return RedirectToAction(nameof(ListEvent));
            }

            return View(eventModel);
        }


        public  IActionResult Update(int id)
        {
            var event_ = _eventService.GetEventById(id);

            if (event_ == null)
            {
                return NotFound();  
            }

            var events = new EventViewModel
            {
                EventID = event_.EventID,
                NameEvent = event_.NameEvent,
                Date = event_.Date,
                Location = event_.Location,
                description = event_.description
            };
            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, EventViewModel eventModel)
        {

            if (ModelState.IsValid)
            {
                var events = new Event
                {
                    EventID = eventModel.EventID,
                    NameEvent = eventModel.NameEvent,
                    Date = eventModel.Date,
                    Location = eventModel.Location,
                    description = eventModel.description
                };

                await _eventService.UpdateEvent(id, events);
                return RedirectToAction(nameof(ListEvent));
            }
            return View(eventModel);
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
