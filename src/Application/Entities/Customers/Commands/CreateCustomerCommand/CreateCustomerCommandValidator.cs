﻿using PhoneNumbers;

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
            .Must(IsValidNumber)
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

    bool IsValidNumber(CreateCustomerCommand request, long num)
    {
        try
        {
            var phone = PhoneNumberUtil.GetInstance().Parse($"+{request.PhoneCountryCode}{num}", "");
            return true;
        }
        catch
        {
            return false;
        }
    }
}