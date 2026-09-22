using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
// Agregar OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/", () => Results.Redirect("/scalar/v1"));

// Configuración de OpenAPI y Scalar
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// HTTPS
app.UseHttpsRedirection();

// Autorización
app.UseAuthorization();

// Controladores
app.MapControllers();

app.Run();