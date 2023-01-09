namespace Application.Entities.Customers.Queries.GetCustomerById;

public class CustomerDto : IMapFrom<Customer>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public byte PhoneCountryCode { get; set; }
    public long PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
}
