using FluentValidation;
using System_Rezerwacji_Biletów.ViewModels;

namespace System_Rezerwacji_Biletów.Validator
{
    public class TicketValidator : AbstractValidator<TicketViewModel>
    {
        public TicketValidator()
        {
            RuleFor(x => x.price).GreaterThan(0).WithMessage("Ticket price must be greater than 0");
        }
    }
}
