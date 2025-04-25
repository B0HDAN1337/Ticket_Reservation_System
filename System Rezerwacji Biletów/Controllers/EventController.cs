using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Service;
using System_Rezerwacji_Biletów.ViewModels;


namespace System_Rezerwacji_Biletów.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IValidator<EventViewModel> _eventValidator;
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IValidator<EventViewModel> validator, IMapper mapper)
        {
            _eventService = eventService;
            _eventValidator = validator;
            _mapper = mapper;
        }

        public IActionResult ListEvent()
        {
            var event_ = _eventService.GetAllEvents();

            var eventViewModel = _mapper.Map<List<EventViewModel>>(event_);

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
            ValidationResult eventValidation = _eventValidator.Validate(eventModel);

            if (!eventValidation.IsValid)
            {
                foreach(var errors in eventValidation.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }
                return View(eventModel);
            }
            else
            {
                var events = _mapper.Map<Event>(eventModel);

                _eventService.CreateEvent(events);
                return RedirectToAction(nameof(ListEvent));
            }
        }


        public  IActionResult Update(int id)
        {
            var event_ = _eventService.GetEventById(id);

            if (event_ == null)
            {
                return NotFound();  
            }

            var events = _mapper.Map<EventViewModel>(event_);
            return View(events);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, EventViewModel eventModel)
        {
            ValidationResult eventValidation = _eventValidator.Validate(eventModel);
            
            if (!eventValidation.IsValid)
            {
                foreach (var errors in eventValidation.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }
                return View(eventModel);
            }
            else
            {
                var events = _mapper.Map<Event>(eventModel);

                await _eventService.UpdateEvent(id, events);
                return RedirectToAction(nameof(ListEvent));
            }
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
