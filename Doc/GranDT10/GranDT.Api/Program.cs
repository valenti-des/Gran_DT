using System.Data;
using GranDT.Aplicacion;
using GranDT.Aplicacion.IRepo;
using GranDT.Aplicacion.Servicios;
using GranDT.Dominio;
using GranDT.Infraestructura;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Gran DT API",
        Version = "v1",
        Description = "API de plantillas, futbolistas y puntuaciones."
    });
    var xml = Path.Combine(AppContext.BaseDirectory, "GranDT.Api.xml");
    if (File.Exists(xml)) options.IncludeXmlComments(xml);
});

builder.Services.AddScoped<IDbConnection>(_ => new MySqlConnection(
    builder.Configuration.GetConnectionString("GranDT")
    ?? throw new InvalidOperationException("Configure ConnectionStrings__GranDT mediante una variable de entorno.")));

builder.Services.AddScoped<IEquipoRepositorio, EquipoRepositorio>();
builder.Services.AddScoped<IFutbolistaRepositorio, FutbolistaRepositorio>();
builder.Services.AddScoped<IPuntuacionRepositorio, PuntuacionRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IPlantillaRepositorio, PlantillaRepositorio>();
builder.Services.AddScoped<IPlantillaFutbolistaRepositorio, PlantillaFutbolistaRepositorio>();
builder.Services.AddScoped<IHashContrasenas, HashContrasenas>();
builder.Services.AddScoped<EquipoServicio>();
builder.Services.AddScoped<FutbolistaServicio>();
builder.Services.AddScoped<PuntuacionServicio>();
builder.Services.AddScoped<UsuarioServicio>();
builder.Services.AddScoped<PlantillaServicio>();
builder.Services.AddScoped<PlantillaFutbolistaServicio>();

var app = builder.Build();

app.UseExceptionHandler(handler => handler.Run(async context =>
{
    var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
    var (status, title) = error switch
    {
        ReglaNegocioException => (StatusCodes.Status400BadRequest, "Los datos enviados no son válidos."),
        RecursoNoEncontradoException => (StatusCodes.Status404NotFound, "El recurso solicitado no existe."),
        ConflictoException => (StatusCodes.Status409Conflict, "La operación entra en conflicto con una regla."),
        MySqlException { Number: 1062 } => (StatusCodes.Status409Conflict, "El recurso ya existe o está duplicado."),
        _ => (StatusCodes.Status500InternalServerError, "Ocurrió un error interno.")
    };
    context.Response.StatusCode = status;
    context.Response.ContentType = "application/problem+json";
    await context.Response.WriteAsJsonAsync(new ProblemDetails
    {
        Status = status,
        Title = title,
        Detail = status == StatusCodes.Status500InternalServerError ? null : error?.Message
    });
}));

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
