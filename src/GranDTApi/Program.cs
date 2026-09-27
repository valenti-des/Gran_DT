using Scalar.AspNetCore;
using System.Data;
using Biblio.IRepo;
using BiblioConDapper;
using GranDTApi.Servicios;
using MySqlConnector;


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
builder.Services.AddScoped<IRepoTipoFutbolista, RepoTipoFutbolista>();
builder.Services.AddScoped<ITipoFutbolistaServicio, TipoFutbolistaServicio>();

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