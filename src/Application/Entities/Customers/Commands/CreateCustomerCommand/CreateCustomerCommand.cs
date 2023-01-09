namespace Application.Entities.Customers.Commands.CreateCustomerCommand;

public class CreateCustomerCommand : IRequest<ApiResult<int>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
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
        request.Email = request.Email.ToLower();
        var entity = new Customer()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            BankAccountNumber = request.BankAccountNumber,
            DateOfBirth = request.DateOfBirth.ToUniversalTime(),
            PhoneNumber = request.PhoneNumber
        };

        await _context.Customers.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<int>()
            .WithData(entity.Id);
    }
}