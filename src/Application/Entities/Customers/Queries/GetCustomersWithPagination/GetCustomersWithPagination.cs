using Application.Common.Models;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Entities.Customers.Queries.GetCustomersWithPagination;

public class GetCustomersWithPagination : IRequest<ApiResult<PaginatedList<CustomerPaginationDto>>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

internal class GetCustomersWithPaginationHandler : IRequestHandler<GetCustomersWithPagination, ApiResult<PaginatedList<CustomerPaginationDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersWithPaginationHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResult<PaginatedList<CustomerPaginationDto>>> Handle(GetCustomersWithPagination request, CancellationToken cancellationToken)
    {
        var data = await _context.Customers
            .ProjectTo<CustomerPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return new ApiResult<PaginatedList<CustomerPaginationDto>>()
            .WithData(data);
    }
}
