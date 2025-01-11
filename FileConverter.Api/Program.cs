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
        policy.AllowAnyOrigin()  
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(); 
}

app.MapConverterEndpoints();
app.UseMiddleware<ApiKeyMiddleware>();
app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
