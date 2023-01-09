using Microsoft.EntityFrameworkCore;

namespace Application.Entities.Customers.Commands.EditCustomerCommand;

public class EditCustomerCommand : IRequest<ApiResult<bool>>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public byte PhoneCountryCode { get; set; }
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

        entity.DateOfBirth = request.DateOfBirth.ToUniversalTime();
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