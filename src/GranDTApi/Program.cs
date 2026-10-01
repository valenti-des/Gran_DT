using Scalar.AspNetCore;
using System.Data;
using BiblioConDapper;
using Biblio.IRepo;
using MySqlConnector;
using Servicios;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthorization();
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IDbConnection>(_ =>
{
    var connectionString = builder.Configuration.GetConnectionString("GranDT");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException(
            "Configura ConnectionStrings:GranDT antes de usar los endpoints que consultan la base de datos.");
    }

    return new MySqlConnection(connectionString);
});
builder.Services.AddScoped<IEquipoRepository, RepoEquipo>();
builder.Services.AddScoped<IEquipoService, EquipoService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();