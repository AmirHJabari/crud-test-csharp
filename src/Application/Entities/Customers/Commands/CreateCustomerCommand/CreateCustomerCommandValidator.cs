using Application.Common;

namespace Application.Entities.Customers.Commands.CreateCustomerCommand;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(b => b.FirstName)
            .Length(2, 50)
            .NotEmpty();

        RuleFor(b => b.LastName)
            .Length(2, 50)
            .NotEmpty();

        RuleFor(b => b.DateOfBirth)
            .NotEmpty();

        RuleFor(b => b.PhoneNumber)
            .Must((x, phone) => Validations.IsValidPhoneNumber(x.PhoneCountryCode, phone))
                .WithMessage(c => $"'+{c.PhoneCountryCode} {c.PhoneNumber}' is not a valid phone number.")
            .NotEmpty();
        
        RuleFor(b => b.Email)
            .EmailAddress()
            .MaximumLength(255)
            .NotEmpty();

        RuleFor(b => b.BankAccountNumber)
            .CreditCard()
            .NotEmpty();
    }
}