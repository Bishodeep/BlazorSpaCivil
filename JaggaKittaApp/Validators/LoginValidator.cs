using FluentValidation;
using JaggaKittaApp.Models;

namespace JaggaKittaApp.Validators;

public class LoginFluentValidator : AbstractValidator<LoginModel>
{
    public LoginFluentValidator()
    {

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(async (value, cancellationToken) => await IsUniqueAsync(value));


        RuleFor(x => x.Password)
            .NotEmpty()
            .Length(1,100);
    }

    private async Task<bool> IsUniqueAsync(string email)
    {
        await Task.Delay(2000);
        return email.ToLower() != "test@test.com";
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<LoginModel>.CreateWithOptions((LoginModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}