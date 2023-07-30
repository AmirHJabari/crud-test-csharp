using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiControllerBase<TController> : ControllerBase
    where TController : ApiControllerBase<TController>
{
    private ISender _mediator = null!;
    private ILogger<TController> _logger = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    protected ILogger<TController> Logger => _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<TController>>();
}
