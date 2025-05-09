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
            app.MapGet("/api/region", () => // async (RegionSP regionSP) =>
            {
                //var regiones = await regionSP.GetRegionesAsync();
                List<Region> regiones = new List<Region>
                {
                    new Region { IdRegion = 1, NombreRegion = "Regi�n de Valpara�so" },
                    new Region { IdRegion = 2, NombreRegion = "Regi�n Metropolitana" },
                    new Region { IdRegion = 3, NombreRegion = "Regi�n del Libertador General Bernardo O'Higgins" }
                };
                return Results.Ok(regiones);
            }).WithName("GetRegiones").WithOpenApi();

            // Endpoint: Informaci�n de una regi�n
            app.MapGet("/api/region/{IdRegion}", async (int IdRegion, RegionSP regionSP) =>
            {
                //var region = await regionSP.GetRegionByIdAsync(IdRegion);
                var region = new Region { IdRegion = 1, NombreRegion = "Regi�n de Valpara�so" };
                if (region == null)
                {
                    return Results.NotFound($"No se encontr� la regi�n con Id {IdRegion}");
                }
                return Results.Ok(region);
            }).WithName("GetRegionById").WithOpenApi();
        }
    }
}
