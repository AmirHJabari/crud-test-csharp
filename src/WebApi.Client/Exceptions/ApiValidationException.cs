using FluentValidation.Results;
using System;

namespace WebApi.Client.Exceptions;

public class ApiValidationException : Exception
{
    public ApiValidationException()
        : base("One or more validation failures have occurred.")
    {
        Result = new ApiResult<IDictionary<string, string[]>>(false, Message, new Dictionary<string, string[]>());
    }

    public ApiValidationException(ApiResult<IDictionary<string, string[]>> apiResult)
        : base("One or more validation failures have occurred.")
    {
        Result = apiResult;
    }

    public ApiResult<IDictionary<string, string[]>> Result { get;private set; }
}
