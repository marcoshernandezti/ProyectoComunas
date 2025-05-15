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
            // Endpoint: Listado de comunas de la regi�n especificada
            app.MapGet("/api/region/{IdRegion}/comuna", () =>
            {
                var comunas = new List<Comuna>
                {
                        new Comuna { IdComuna = 1, IdRegion = 1, NombreComuna = "Valpara�so", InformacionAdicional = "Puerto principal de Chile" },
                        new Comuna { IdComuna = 2, IdRegion = 1, NombreComuna = "Vi�a del Mar", InformacionAdicional = "Conocida como la Ciudad Jard�n" }
                };
                return Results.Ok(comunas);
            })
            .WithName("GetComunasByRegion")
            .WithOpenApi(x => new OpenApiOperation(x)
            {
                Summary = "Obtiene el listado de comunas de una regi�n espec�fica.",
                Description = "Devuelve una lista de comunas asociadas a una regi�n seg�n su identificador.",
            });

            // Endpoint: Informaci�n de una comuna
            app.MapGet("/api/region/{IdRegion}/comuna/{IdComuna}", async (int IdRegion, int IdComuna, ComunaSP comunaSP) =>
            {
                var comuna = await comunaSP.GetComunaByIdAsync(IdRegion, IdComuna);
                if (comuna == null)
                {
                    return Results.NotFound($"No se encontr� la comuna con Id {IdComuna} en la regi�n {IdRegion}");
                }
                return Results.Ok(comuna);
            })
            .WithName("GetComunaById")
            .WithOpenApi(operation =>
            {
                operation.Summary = "Obtiene informaci�n de una comuna espec�fica.";
                operation.Description = "Devuelve los detalles de una comuna seg�n su identificador y el de su regi�n.";
                operation.Responses["200"] = new() { Description = "Informaci�n de la comuna obtenida exitosamente." };
                operation.Responses["404"] = new() { Description = "No se encontr� la comuna con los Ids proporcionados." };
                return operation;
            });

            // Endpoint: Actualizar informaci�n de una comuna
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
                operation.Summary = "Actualiza la informaci�n de una comuna.";
                operation.Description = "Permite actualizar los datos de una comuna espec�fica en una regi�n.";
                operation.RequestBody.Description = "Los datos actualizados de la comuna.";
                operation.Responses["200"] = new() { Description = "Comuna actualizada exitosamente." };
                operation.Responses["400"] = new() { Description = "No se pudo actualizar la comuna." };
                return operation;
            });
        }
    }
}
