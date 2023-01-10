using Microsoft.EntityFrameworkCore;

namespace Application.Entities.Customers.Commands.EditCustomerCommand;

public class EditCustomerCommand : IRequest<ApiResult<bool>>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public short PhoneCountryCode { get; set; }
    public long PhoneNumber { get; set; }
    public string BankAccountNumber { get; set; }
}

public class CreateCustomerCommandHandler : IRequestHandler<EditCustomerCommand, ApiResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Customer), request.Id);

        if (!entity.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase))
        {
            var any = await _context.Customers.AnyAsync(c => c.Email == request.Email.ToLower());
            if (any)
                throw new ValidationException($"The email '{request.Email}' already exist in the system!");
        }

        var dob = request.DateOfBirth;
        var dobUtc = new DateTime(dob.Year, dob.Month, dob.Day, 0, 0, 0, DateTimeKind.Utc);

        if (!entity.FirstName.Equals(request.FirstName, StringComparison.OrdinalIgnoreCase) ||
            !entity.LastName.Equals(request.LastName, StringComparison.OrdinalIgnoreCase) ||
            !entity.DateOfBirth.Equals(dobUtc))
        {
            var any = await _context.Customers.AnyAsync(c => c.FirstName == request.FirstName &&
                                c.LastName == request.LastName &&
                                c.DateOfBirth == dobUtc);
            if (any)
                throw new ValidationException($"Someone with the same 'First name', 'Last name' and 'Date of birth' is already registered in the system!");
        }

        entity.DateOfBirth = dobUtc;
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Email = request.Email;
        entity.PhoneCountryCode = request.PhoneCountryCode;
        entity.PhoneNumber = request.PhoneNumber;
        entity.BankAccountNumber = request.BankAccountNumber;


        _context.Customers.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        return new ApiResult<bool>()
            .WithData(true);
    }
}