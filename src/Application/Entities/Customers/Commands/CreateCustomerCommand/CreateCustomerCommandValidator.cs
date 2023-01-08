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
            .Length(9, 15)
            .NotEmpty();
        
        RuleFor(b => b.Email)
            .MaximumLength(255)
            .EmailAddress()
            .NotEmpty();

        RuleFor(b => b.BankAccountNumber)
            .Length(16)
            .NotEmpty();
    }
}