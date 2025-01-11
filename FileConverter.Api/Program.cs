using FileConverter.Api.Configuration;
using FileConverter.Api.Controllers;
using FileConverter.Infrastructure.Extensions;
using FileConverter.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddValidatorServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Altere para .WithOrigins("https://example.com") para maior seguran�a
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSwagger();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(); // Usa o m�todo de extens�o para Swagger UI
}

app.MapConverterEndpoints();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
