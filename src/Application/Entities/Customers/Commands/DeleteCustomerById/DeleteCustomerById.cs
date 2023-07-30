namespace Application.Entities.Customers.Commands.DeleteCustomerById;

public class DeleteCustomerById : IRequest<ApiResult<bool>>
{
    public int Id { get; set; }
}

public class DeleteCustomerByIdHandler : IRequestHandler<DeleteCustomerById, ApiResult<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerByIdHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<bool>> Handle(DeleteCustomerById request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Customer), request.Id);

        _context.Customers.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<bool>()
            .WithData(true);
    }
}