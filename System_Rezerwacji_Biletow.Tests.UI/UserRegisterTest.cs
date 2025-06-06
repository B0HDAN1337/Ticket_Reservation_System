using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;
using System_Rezerwacji_Biletow.Areas.Identity.Pages.Account;

public class RegisterModelTests
{
    private IList<ValidationResult> Validate(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }

    [Fact]
    public void InputModel_InvalidEmail_FailsValidation()
    {
        var input = new RegisterModel.InputModel
        {
            Email = "notemail",
            Password = "password123",
            ConfirmPassword = "password123"
        };

        var results = Validate(input);
        Assert.True(results.Any(r => r.MemberNames.Contains(nameof(RegisterModel.InputModel.Email))));
    }

    [Fact]
    public void InputModel_ValidData_PassesValidation()
    {
        var input = new RegisterModel.InputModel
        {
            Email = "test@example.com",
            Password = "password123",
            ConfirmPassword = "password123"
        };

        var results = Validate(input);
        Assert.Empty(results);
    }
}


