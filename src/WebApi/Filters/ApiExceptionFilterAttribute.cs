using Application.Common.Exceptions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private static IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    static ApiExceptionFilterAttribute()
    {
        // Register known exception types and handlers.
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException }
            };
    }

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        context.Result = new BadRequestObjectResult(exception.AsApiResult());

        context.ExceptionHandled = true;
    }

    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };

        context.Result = new BadRequestObjectResult(details);

        context.ExceptionHandled = true;
    }

    private static void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;
        context.Result = new NotFoundObjectResult(exception.AsApiResult());

        context.ExceptionHandled = true;
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new ApiResult(false, "An error occurred while processing your request."))
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}
