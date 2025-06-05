using FluentValidation;
using System_Rezerwacji_Biletów.ViewModels;

namespace System_Rezerwacji_Biletów.Validator
{
    public class EventValidator : AbstractValidator<EventViewModel>
    {
        public EventValidator()
        {
            RuleFor(x => x.NameEvent).NotEmpty().Length(0, 50).WithMessage("Name must be less than 20 letters");
            RuleFor(x => x.Date).GreaterThan(x => DateTime.Now).WithMessage("The date of event should not be today or any past date.");
            RuleFor(x => x.Location).NotEmpty().Length(0, 100).WithMessage("Location must be less than 50 letters");
            RuleFor(x => x.description).NotEmpty().Length(0, 255).WithMessage("Description must be less than 255 letters");
        }
    }
}
