using FileConverter.Application.Services.FileConverterService;
using FileConverter.Application.Services.FileConverterService.Models;
using FileConverter.Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FileConverter.Api.Controllers
{
    public static class ConverterEndpoints
    {
        const string jsonContenType = "application/json";
        const string multipartContenType = "multipart/form-data";
        public static void MapConverterEndpoints(this WebApplication app)
        {
            var converterGroup = app.MapGroup("api/v1/converter").WithTags("converter");

            converterGroup.MapPost("/base64", async (IValidator<FileUploadModel> validator, [FromForm] FileUploadModel request, IFileConverterService service) =>
            {
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.ToDictionary());

                var base64 = await service.ConvertToBase64Async(request.File);

                return Results.Ok(new ApiResponse<FileBase64ViewModel>
                {
                    Success = true,
                    Data = new FileBase64ViewModel
                    {
                        FileBase64 = base64
                    }
                });
            })
            .WithName("ConvertToBase64")
            .Accepts<FileUploadModel>(multipartContenType)
            .Produces<ApiResponse<FileUploadModel>>(StatusCodes.Status200OK)
            .ProducesValidationProblem()
            .WithOpenApi(operation =>
            {
                operation.Description = "Converts a file to base64.";
                operation.Summary = "Converts a file to base64.";
                return operation;
            });
        }
    }
}
