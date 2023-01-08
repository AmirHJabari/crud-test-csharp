using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects;

public class ApiResult<TData> : ApiResult
{
    public ApiResult()
        : base()
    {
        this.Data = default;
    }

    public ApiResult(bool status) 
        : base(status)
    {
        this.Data = default;
    }

    public ApiResult(bool success, string message, TData data)
       : base(success, message)
    {
        this.Data = data;
    }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public TData Data { get; set; }

    public override ApiResult<TData> WithStatus(bool success = true)
    {
        this.IsSuccess = success;
        return this;
    }

    public override ApiResult<TData> WithMessage(string message)
    {
        this.Message = message;
        return this;
    }

    public ApiResult<TData> WithData(TData data)
    {
        this.Data = data;
        return this;
    }
}

public class ApiResult
{
    public ApiResult()
        : this(true)
    { }

    public ApiResult(bool status)
    {
        this.IsSuccess = status;
        this.Message = status ? "Operation was successful" : "Operation was not successful";
    }

    public ApiResult(bool success, string message)
    {
        this.IsSuccess = success;
        this.Message = message;
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public virtual ApiResult WithStatus(bool success = true)
    {
        this.IsSuccess = success;
        return this;
    }

    public virtual ApiResult WithMessage(string message)
    {
        this.Message = message;
        return this;
    }
}
