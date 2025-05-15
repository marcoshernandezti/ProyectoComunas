using Microsoft.OpenApi.Models;
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
            app.MapGet("/api/region/{IdRegion}/comuna", () =>
            {
                var comunas = new List<Comuna>
                {
                        new Comuna { IdComuna = 1, IdRegion = 1, NombreComuna = "Valparaíso", InformacionAdicional = "Puerto principal de Chile" },
                        new Comuna { IdComuna = 2, IdRegion = 1, NombreComuna = "Viña del Mar", InformacionAdicional = "Conocida como la Ciudad Jardín" }
                };
                return Results.Ok(comunas);
            })
            .WithName("GetComunasByRegion")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Obtiene el listado de comunas de una región específica.",
                Description = "Devuelve una lista de comunas asociadas a una región según su identificador.",
            });

            // Endpoint: Información de una comuna
            app.MapGet("/api/region/{IdRegion}/comuna/{IdComuna}", async (int IdRegion, int IdComuna, ComunaSP comunaSP) =>
            {
                var comuna = await comunaSP.GetComunaByIdAsync(IdRegion, IdComuna);
                if (comuna == null)
                {
                    return Results.NotFound($"No se encontró la comuna con Id {IdComuna} en la región {IdRegion}");
                }
                return Results.Ok(comuna);
            })
            .WithName("GetComunaById")
            .WithOpenApi(operation =>
            {
                operation.Summary = "Obtiene información de una comuna específica.";
                operation.Description = "Devuelve los detalles de una comuna según su identificador y el de su región.";
                operation.Responses["200"] = new() { Description = "Información de la comuna obtenida exitosamente." };
                operation.Responses["404"] = new() { Description = "No se encontró la comuna con los Ids proporcionados." };
                return operation;
            });

            // Endpoint: Actualizar información de una comuna
            app.MapPost("/api/region/{IdRegion}/comuna", async (int IdRegion, Comuna comuna, ComunaSP comunaSP) =>
            {
                var result = await comunaSP.UpdateComunaAsync(IdRegion, comuna);
                if (result > 0)
                {
                    return Results.Ok("Comuna actualizada correctamente");
                }
                return Results.BadRequest("No se pudo actualizar la comuna");
            })
            .WithName("UpdateComuna")
            .WithOpenApi(operation =>
            {
                operation.Summary = "Actualiza la información de una comuna.";
                operation.Description = "Permite actualizar los datos de una comuna específica en una región.";
                operation.RequestBody.Description = "Los datos actualizados de la comuna.";
                operation.Responses["200"] = new() { Description = "Comuna actualizada exitosamente." };
                operation.Responses["400"] = new() { Description = "No se pudo actualizar la comuna." };
                return operation;
            });
        }
    }
}
