using ProyectoComunas.Datos.Models;
using ProyectoComunas.Datos.StoredProcedures;

namespace ProyectoComunas.API.Endpoints
{
    public static class ComunaEndpoints
    {
        /// <summary>
        /// Endpoints para Comunas
        /// </summary>
        public static void MapComunaEndpoints(this IEndpointRouteBuilder app)
        {
            // Endpoint: Listado de comunas de la región especificada
            app.MapGet("/api/region/{IdRegion}/comuna", () => // async (int IdRegion, ComunaSP comunaSP) =>
            {
                //var comunas = await comunaSP.GetComunasByRegionAsync(IdRegion);   
                var comunas = new List<Comuna>
                {
                    new Comuna { IdComuna = 1, IdRegion = 1, NombreComuna = "Valparaíso", InformacionAdicional = "Puerto principal de Chile" },
                    new Comuna { IdComuna = 2, IdRegion = 1, NombreComuna = "Viña del Mar", InformacionAdicional = "Conocida como la Ciudad Jardín" }
                };
                return Results.Ok(comunas);

            }).WithName("GetComunasByRegion").WithOpenApi();

            // Endpoint: Información de una comuna
            app.MapGet("/api/region/{IdRegion}/comuna/{IdComuna}", async (int IdRegion, int IdComuna, ComunaSP comunaSP) =>
            {
                var comuna = await comunaSP.GetComunaByIdAsync(IdRegion, IdComuna);
                if (comuna == null)
                {
                    return Results.NotFound($"No se encontró la comuna con Id {IdComuna} en la región {IdRegion}");
                }
                return Results.Ok(comuna);
            }).WithName("GetComunaById").WithOpenApi();

            // Endpoint: Actualizar información de una comuna
            app.MapPost("/api/region/{IdRegion}/comuna", async (int IdRegion, Comuna comuna, ComunaSP comunaSP) =>
            {
                var result = await comunaSP.UpdateComunaAsync(IdRegion, comuna);
                if (result > 0)
                {
                    return Results.Ok("Comuna actualizada correctamente");
                }
                return Results.BadRequest("No se pudo actualizar la comuna");
            }).WithName("UpdateComuna").WithOpenApi();
        }
    }
}
