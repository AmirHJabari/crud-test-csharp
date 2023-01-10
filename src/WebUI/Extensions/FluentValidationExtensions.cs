namespace FluentValidation;

public static class FluentValidationExtensions
{
    public static Func<object, string, Task<IEnumerable<string>>> ValidateMudForm<T>(this AbstractValidator<T> validator)
    {
        return async (model, propertyName) =>
            {
                var result = await validator.ValidateAsync(ValidationContext<T>.CreateWithOptions((T)model, x => x.IncludeProperties(propertyName)));
                if (result.IsValid)
                    return Array.Empty<string>();
                return result.Errors.Select(e => e.ErrorMessage);
            };
    }

    public static IEnumerable<string> GetValidationErrors<T>(this AbstractValidator<T> validator, T model)
    {
        var res = validator.Validate(model);
        return GetValidationErrors(res);
    }

    public static async Task<IEnumerable<string>> GetValidationErrorsAsync<T>(this AbstractValidator<T> validator, T model)
    {
        var res = await validator.ValidateAsync(model);
        return GetValidationErrors(res);
    }

    static IEnumerable<string> GetValidationErrors(FluentValidation.Results.ValidationResult result)
    {
        if (result.Errors.Any())
        {
            return result.Errors.Select(x => x.ErrorMessage).ToList();
        }
        else
        {
            return null;
        }
    }

    
    public static IDictionary<string, string[]> GetValidationErrorsAsDictionary<T>(this AbstractValidator<T> validator, T model)
    {
        var res = validator.Validate(model);
        return GetValidationErrorsAsDictionary(res);
    }

    public static async Task<IDictionary<string, string[]>> GetValidationErrorsAsDictionaryAsync<T>(this AbstractValidator<T> validator, T model)
    {
        var res = await validator.ValidateAsync(model);
        return GetValidationErrorsAsDictionary(res);
    }

    static IDictionary<string, string[]> GetValidationErrorsAsDictionary(FluentValidation.Results.ValidationResult result)
    {
        if (result.Errors.Any())
        {
            return result.Errors
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
        else
        {
            return null;
        }
    }
}