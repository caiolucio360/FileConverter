using FluentValidation.Results;

namespace FileConverter.Infrastructure.Extensions
{
    public static class ValidationResultExtensions
    {
        public static List<object> ToErrorList(this ValidationResult validationResult)
        {
            return validationResult.Errors
                .Select(e => new
                {
                    propertyName = e.PropertyName,
                    errorMessage = e.ErrorMessage
                })
                .ToList<object>();
        }
    }
}
