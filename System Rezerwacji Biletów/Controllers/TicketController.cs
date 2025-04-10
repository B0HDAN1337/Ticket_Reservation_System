
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Service;
using System_Rezerwacji_Biletów.ViewModels;

namespace System_Rezerwacji_Biletów.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IEventService _eventService;
        private readonly IValidator<TicketViewModel> _ticketValidator;

        public TicketController(ITicketService ticketService, IEventService eventService, IValidator<TicketViewModel> validator)
        {
            _ticketService = ticketService;
            _eventService = eventService;
            _ticketValidator = validator;
        }

        public IActionResult ListTicket()
        {
            var tickets = _ticketService.GetAllTickets();

            var ticketViewModel = tickets.Select(tickets => new TicketViewModel
            {
                TicketID = tickets.TicketID,
                EventID = tickets.EventID,
                price = tickets.price,
                NameEvent = tickets.Event.NameEvent
            });

            return View(ticketViewModel);
        }

        public IActionResult Create()
        {
            var events = _eventService.GetAllEvents();

            var model = new TicketViewModel
            {
                Events = events.Select(e => new SelectListItem
                {
                    Value = e.EventID.ToString(),
                    Text = e.NameEvent
                })
            };

            return View(model);
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(TicketViewModel model)
        {
            ValidationResult ticketValidation = _ticketValidator.Validate(model);
            
            if (!ticketValidation.IsValid)
            {
                foreach(var errors in ticketValidation.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }

                var events = _eventService.GetAllEvents();
                model.Events = events.Select(e => new SelectListItem
                {
                    Value = e.EventID.ToString(),
                    Text = e.NameEvent
                });
                return View(model);
            }
            else
            {
                var ticket = new Ticket
                {
                    EventID = model.EventID,
                    price = model.price
                };

                _ticketService.CreateTicket(ticket);
                return RedirectToAction(nameof(ListTicket));
            }
        }

        public  IActionResult Update(int id)
        {
            var existTicket = _ticketService.GetTicketById(id);

            if (existTicket == null)
            {
                return NotFound();
            }

            var model = new TicketViewModel
            {
                TicketID = existTicket.TicketID,
                EventID = existTicket.EventID,
                price = existTicket.price,
                NameEvent = existTicket.Event.NameEvent
            };

            var events = _eventService.GetAllEvents();
            ViewBag.Events = new SelectList(events, "EventID", "NameEvent");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TicketViewModel model)
        {

            ValidationResult ticketValidation = _ticketValidator.Validate(model);
            
            if(!ticketValidation.IsValid)
            {
                foreach(var errors in ticketValidation.Errors)
                {
                    ModelState.AddModelError("", errors.ErrorMessage);
                }

                var events = _eventService.GetAllEvents();
                ViewBag.Events = new SelectList(events, "EventID", "NameEvent");
                return View(model);

            }
            else
            {
                var ticket = new Ticket
                {
                    TicketID = model.TicketID,
                    EventID = model.EventID,
                    price = model.price
                };

                await _ticketService.UpdateTicket(id, ticket);
                return RedirectToAction(nameof(ListTicket));
            }            
        }

        public IActionResult Delete(int id)
        {
            var ticket_ = _ticketService.GetTicketById(id);

            if(ticket_ == null)
            {
                return NotFound();
            }

            _ticketService.DeleteTIcket(id);

            return RedirectToAction(nameof(ListTicket));
        }

    }
}
