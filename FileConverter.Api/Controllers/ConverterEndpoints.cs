using FileConverter.Api.Models;
using FileConverter.Application.Services.FileConverterService;
using FileConverter.Application.Services.FileConverterService.Models;
using FluentValidation;

namespace FileConverter.Api.Controllers
{
    public static class ConverterEndpoints
    {
        const string jsonContenType = "application/json";
        public static void MapConverterEndpoints(this WebApplication app)
        {
            var converterGroup = app.MapGroup("api/v1/converter").WithTags("converter");

            converterGroup.MapPost("/base64", async (IValidator<FileUploadModel> validator, FileUploadModel request, IFileConverterService service) =>
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
            .Accepts<FileUploadModel>(jsonContenType)
            .Produces<FileUploadModel>(StatusCodes.Status200OK)
            .ProducesValidationProblem();
        }
    }
}
