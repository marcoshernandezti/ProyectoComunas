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
            app.MapGet("/api/region/{IdRegion}/comuna", async (int IdRegion, ComunaSP comunaSP) =>
            {
                var comunas = await comunaSP.GetComunasByRegionAsync(IdRegion);
                if (comunas == null || comunas.Count == 0)
                {
                    return Results.NotFound($"No se encontró las comunas en la región {IdRegion}");
                }
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
            app.MapPatch("/api/region/{IdRegion}/comuna", async (int IdRegion, Comuna comuna, ComunaSP comunaSP) =>
            {
                //// Buscar si la comuna ya existe
                //var comunaExistente = comuna.IdComuna > 0
                //    ? await comunaSP.GetComunaByIdAsync(IdRegion, comuna.IdComuna)
                //    : null;

                //if (comunaExistente != null)
                //{
                //    // Actualizar comuna existente
                //    var result = await comunaSP.UpdateComunaAsync(IdRegion, comuna);
                //    if (result > 0)
                //        return Results.Ok("Comuna actualizada correctamente");
                //    return Results.BadRequest("No se pudo actualizar la comuna");
                //}
                //else
                //{
                //    // Crear nueva comuna (debería implementarse un método InsertComunaAsync en ComunaSP)
                //    // Aquí se asume que existe dicho método, si no, debe implementarse.
                //    if (comuna.IdRegion == null) comuna.IdRegion = IdRegion;
                //    var result = await comunaSP.UpdateComunaAsync(IdRegion, comuna);
                //    if (result > 0)
                //        return Results.Ok("Comuna creada correctamente");
                //    return Results.BadRequest("No se pudo crear la comuna");
                //}
                return Results.StatusCode(StatusCodes.Status501NotImplemented);
            })
                .WithName("UpsertComuna")
                .WithOpenApi(operation =>
                {
                    operation.Summary = "No implementado: Crea o actualiza la información de una comuna.";
                    operation.Description = "Este endpoint aún no está implementado.";
                    operation.Responses["501"] = new() { Description = "No implementado." };
                    return operation;
                });
        //})
        //    .WithName("UpsertComuna")
        //    .WithOpenApi(operation =>
        //    {
        //        operation.Summary = "Crea o actualiza la información de una comuna.";
        //        operation.Description = "Permite crear una nueva comuna o actualizar los datos de una existente en una región.";
        //        operation.RequestBody.Description = "Los datos de la comuna a crear o modificar.";
        //        operation.Responses["200"] = new() { Description = "Comuna creada o actualizada exitosamente." };
        //        operation.Responses["400"] = new() { Description = "No se pudo crear o actualizar la comuna." };
        //        return operation;
        //    });
        }
    }
}
