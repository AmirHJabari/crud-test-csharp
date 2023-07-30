namespace WebApi.Client.Exceptions;

public class ApiNotFoundException : ApiBaseException
{
    public ApiNotFoundException(string message)
        : base(message)
    {
    }

    public ApiNotFoundException(string message, ApiResult apiResult)
        : base(message, apiResult)
    {
    }
}
