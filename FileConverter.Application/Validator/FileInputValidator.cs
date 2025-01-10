using FileConverter.Application.Services.FileConverterService.Models;
using FluentValidation;

namespace FileConverter.Application.Validator
{
    public class FileInputValidator : AbstractValidator<FileUploadModel>
    {
        public FileInputValidator()
        {
            RuleFor(model => model.File)
                .NotNull()
                .WithMessage("File is required.");

            RuleFor(model => model.File.Length)
                .LessThanOrEqualTo(5 * 1024 * 1024)
                .WithMessage("File size must be less than 5MB.");
        }
    }
}
