using Microsoft.EntityFrameworkCore;

namespace Application.Entities.Customers.Commands.CreateCustomerCommand;

public class CreateCustomerCommand : IRequest<ApiResult<int>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public short PhoneCountryCode { get; set; }
    public long PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ApiResult<int>>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<int>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var dob = request.DateOfBirth;
        var dobUtc = new DateTime(dob.Year, dob.Month, dob.Day, 0, 0, 0, DateTimeKind.Utc);
        var entity = new Customer()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BankAccountNumber = request.BankAccountNumber,
            DateOfBirth = dobUtc,
            PhoneNumber = request.PhoneNumber,
            PhoneCountryCode = request.PhoneCountryCode
        };

        if (await _context.Customers.AnyAsync(c => c.Email == request.Email.ToLower()))
            throw new ValidationException($"The email '{request.Email}' already exist in the system!");

        if (await _context.Customers.AnyAsync(c =>
                                c.FirstName == request.FirstName &&
                                c.LastName == request.LastName &&
                                c.DateOfBirth == dobUtc))
        {
            throw new ValidationException($"Someone with the same 'First name', 'Last name' and 'Date of birth' is already registered in the system!");
        }

        await _context.Customers.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<int>()
            .WithData(entity.Id);
    }
}