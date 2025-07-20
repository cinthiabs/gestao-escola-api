using Ardalis.Result;
using FluentValidation.Results;

namespace Application.Extensions;

public static class ValidationResultExtensions
{
    public static Result ToResult(this ValidationResult validationResult)
    {
        if (validationResult.IsValid)
            return Result.Success();

        var errors = validationResult.Errors
        .Select(e => new ValidationError
        {
            Identifier = e.PropertyName,
            ErrorMessage = e.ErrorMessage
        }).ToList();

        return Result.Invalid(errors);
    }
}