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
                    new Region { IdRegion = 1, NombreRegion = "Región de Valparaíso" },
                    new Region { IdRegion = 2, NombreRegion = "Región Metropolitana" },
                    new Region { IdRegion = 3, NombreRegion = "Región del Libertador General Bernardo O'Higgins" }
                };
                return Results.Ok(regiones);
            }).WithName("GetRegiones").WithOpenApi();

            // Endpoint: Información de una región
            app.MapGet("/api/region/{IdRegion}", async (int IdRegion, RegionSP regionSP) =>
            {
                //var region = await regionSP.GetRegionByIdAsync(IdRegion);
                var region = new Region { IdRegion = 1, NombreRegion = "Región de Valparaíso" };
                if (region == null)
                {
                    return Results.NotFound($"No se encontró la región con Id {IdRegion}");
                }
                return Results.Ok(region);
            }).WithName("GetRegionById").WithOpenApi();
        }
    }
}
