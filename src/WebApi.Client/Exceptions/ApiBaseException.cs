namespace WebApi.Client.Exceptions;

public class ApiBaseException : Exception
{
    public ApiBaseException(string message)
        : base(message)
    {
    }

    public ApiBaseException(string message, ApiResult apiResult)
        : base(message)
    {
        Result = apiResult;
    }

    public ApiResult Result { get; private set; }
}
