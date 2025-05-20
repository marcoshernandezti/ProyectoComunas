using ProyectoComunas.Datos.Models;
using ProyectoComunas.Datos.StoredProcedures;

namespace ProyectoComunas.API.Endpoints
{
    public static class RegionEndpoints
    {
        /// <summary>
        /// Endpoints para Regiones
        /// </summary>
        public static void MapRegionEndpoints(this IEndpointRouteBuilder app)
        {
            // Endpoint: Listado de regiones
            app.MapGet("/api/region", static async (RegionSP regionSP) =>
            {
                var regiones = await regionSP.GetRegionesAsync();
                if (regiones == null || regiones.Count == 0)
                {
                    return Results.NotFound("No se encontraron regiones.");
                }
                return Results.Ok(regiones);
            })
            .WithName("GetRegiones")
            .WithOpenApi(operation =>
            {
                operation.Summary = "Obtiene el listado de todas las regiones.";
                operation.Description = "Devuelve una lista de regiones disponibles en el sistema.";
                operation.Responses["200"] = new() { Description = "Lista de regiones obtenida exitosamente." };
                return operation;
            });

            // Endpoint: Información de una región
            app.MapGet("/api/region/{IdRegion}", async (int IdRegion, RegionSP regionSP) =>
            {
                var region = await regionSP.GetRegionByIdAsync(IdRegion);
                if (region == null)
                {
                    return Results.NotFound($"No se encontró la región con Id {IdRegion}");
                }
                return Results.Ok(region);                
                //var region = new Region { IdRegion = 1, NombreRegion = "Región de Valparaíso" };
                //if (region == null)
                //{
                //    return Task.FromResult(Results.NotFound($"No se encontró la región con Id {IdRegion}"));
                //}
                //return Task.FromResult(Results.Ok(region));
            })
            .WithName("GetRegionById")
            .WithOpenApi(operation =>
            {
                operation.Summary = "Obtiene información de una región específica.";
                operation.Description = "Devuelve los detalles de una región según su identificador.";
                operation.Responses["200"] = new() { Description = "Información de la región obtenida exitosamente." };
                operation.Responses["404"] = new() { Description = "No se encontró la región con el Id proporcionado." };
                return operation;
            });
        }
    }
}
