using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Application.Common.Models;
using Application.Entities.Customers.Commands.CreateCustomerCommand;
using Application.Entities.Customers.Queries.GetCustomerById;

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

    [HttpGet]
    public async Task<ActionResult<ApiResult<CustomerDto>>> GetById([FromQuery] GetCustomerById request,
        CancellationToken cancellationToken)
    {
        return await Mediator.Send(request, cancellationToken);
    }
}