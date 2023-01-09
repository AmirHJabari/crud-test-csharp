using Domain.Common;

namespace Domain.Entities;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public byte PhoneCountryCode { get; set; }
    public long PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
}
