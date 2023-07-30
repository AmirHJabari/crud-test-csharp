namespace Application.Entities.Customers.Queries.GetCustomerById;

public class GetCustomerById : IRequest<ApiResult<CustomerDto>>
{
    public int Id { get; set; }
}

public class GetCustomerByIdHandler : IRequestHandler<GetCustomerById, ApiResult<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<CustomerDto>> Handle(GetCustomerById request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers.FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
            throw new NotFoundException(nameof(Customer), request.Id);

        var dto = _mapper.Map<CustomerDto>(entity);

        return new ApiResult<CustomerDto>()
            .WithData(dto);
    }
}