using Scalar.AspNetCore;
using System.Data;
using Biblio;
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
    return new MySqlConnection(connectionString);
});
builder.Services.AddScoped<IRepoEquipo, RepoEquipo>();
builder.Services.AddScoped<IRepoPuntuacion, RepoPuntuacion>();
builder.Services.AddScoped<IRepoUsuario, RepoUsuario>();
builder.Services.AddScoped<IRepoPlantilla, RepoPlantilla>();
builder.Services.AddScoped<IRepoPlantillaFutbolista, RepoPlantillaFutbolista>();
builder.Services.AddScoped<IRepoFutbolista, RepoFutbolista>();
builder.Services.AddScoped<IEstrategiaCodificacion, EstrategiaCodificacion>();
builder.Services.AddScoped<ServiciosEquipo>();
builder.Services.AddScoped<ServiciosPuntuacion>();
builder.Services.AddScoped<ServiciosUsuario>();
builder.Services.AddScoped<ServiciosPlantilla>();
builder.Services.AddScoped<ServiciosFutbolista>();

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