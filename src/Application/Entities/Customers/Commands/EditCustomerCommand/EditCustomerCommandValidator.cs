﻿using PhoneNumbers;

namespace Application.Entities.Customers.Commands.EditCustomerCommand;

public class EditCustomerCommandValidator : AbstractValidator<EditCustomerCommand>
{
    public EditCustomerCommandValidator()
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

    bool IsValidNumber(EditCustomerCommand request, long num)
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