using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using ProyectoComunas.API.Endpoints;
using ProyectoComunas.Datos;
using ProyectoComunas.Datos.Exceptions;
using ProyectoComunas.Datos.StoredProcedures;

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
logger.Debug("Iniciando aplicación");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Configurar NLog
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    // Configurar servicios
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.AddScoped<RegionSP>();
    builder.Services.AddScoped<ComunaSP>();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Middleware de manejo de excepciones
    app.Use(async (context, next) =>
    {
        try
        {
            await next(); // Ejecuta el siguiente middleware o endpoint
        }
        catch (DatosException ex)
        {
            // Error ya registrado en log previamente
            context.Response.StatusCode = 500; // Código de error interno del servidor
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = ex.Message,
                details = ex.InnerException?.Message
            });
        }
        catch (Exception ex)
        {
            // Manejo genérico para otras excepciones
            logger.Error(ex, "Ocurrió un error inesperado");
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                error = "Ocurrió un error inesperado.",
                details = ex.Message
            });
        }
    });

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    // Registrar los endpoints
    app.UseMiddleware<ProyectoComunas.API.Middlewares.ApiKeyMiddleware>();
    app.MapRegionEndpoints();
    app.MapComunaEndpoints();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Error al iniciar la aplicación");
    throw;
}
finally
{
    LogManager.Shutdown();
}