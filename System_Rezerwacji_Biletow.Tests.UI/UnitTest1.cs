using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System_Rezerwacji_Biletów.ViewModels;

public class UserViewModelTests
{
    [Fact]
    public void UserViewModel_Requires_Name_And_LastName()
    {
        var model = new UserViewModel
        {
            Name = "",
            LastName = "",
            BirthDate = DateTime.Now,
            email = "test@example.com"
        };

        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(isValid);
        Assert.Contains(results, r => r.ErrorMessage.Contains("Please enter name"));
        Assert.Contains(results, r => r.ErrorMessage.Contains("Please enter last Name"));
    }
}


