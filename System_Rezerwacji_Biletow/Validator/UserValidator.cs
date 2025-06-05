using FluentValidation;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.ViewModels;

namespace System_Rezerwacji_Biletów.Validator
{
    public class UserValidator : AbstractValidator<UserViewModel>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter your name.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Enter your last name.");
            RuleFor(x => x.BirthDate).LessThan(x => DateTime.Now).WithMessage("The date of birth should not be today or any future date.");
            RuleFor(x => x.email).EmailAddress().WithMessage("The email address must contain special characters.");
        }
    }
}
