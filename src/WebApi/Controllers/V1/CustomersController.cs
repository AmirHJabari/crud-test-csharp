using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.Common.Models;
using Application.Entities.Customers.Commands.CreateCustomerCommand;
using Application.Entities.Customers.Queries.GetCustomerById;
using Application.Entities.Customers.Commands.DeleteCustomerById;
using Application.Entities.Customers.Queries.GetCustomersWithPagination;
using Application.Entities.Customers.Commands.EditCustomerCommand;

namespace WebApi.Controllers.V1;

[ApiVersion("1")]
public class CustomersController : ApiControllerBase<CustomersController>
{
    [HttpPost]
    public async Task<ActionResult<ApiResult<int>>> Create(CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResult<CustomerDto>>> GetById([Required, FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetCustomerById() { Id = id }, cancellationToken);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResult<PaginatedList<CustomerPaginationDto>>>> GetPaginatedList([FromQuery] GetCustomersWithPagination request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpDelete]
    public async Task<ActionResult<ApiResult<bool>>> DeleteById([FromQuery] DeleteCustomerById request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }

    [HttpPut]
    public async Task<ActionResult<ApiResult<bool>>> Edit(EditCustomerCommand request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }
}