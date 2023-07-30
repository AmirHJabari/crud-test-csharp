using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    string _email;
    public string Email
    {
        get => _email;
        set => _email = value.ToLower();
    }

    public DateTime DateOfBirth { get; set; }
    public short PhoneCountryCode { get; set; }
    public long PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
}
