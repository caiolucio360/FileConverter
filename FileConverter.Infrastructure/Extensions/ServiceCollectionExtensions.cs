using FileConverter.Application.Services.FileConverterService;
using FileConverter.Application.Services.FileConverterService.Models;
using FileConverter.Application.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFileConverterService, FileConverterService>();
        }

        public static void AddValidatorServices(this IServiceCollection services)
        {
            services.AddScoped<IValidator<FileUploadModel>, FileInputValidator>();
        }
    }
}
