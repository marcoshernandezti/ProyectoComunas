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
            app.MapGet("/api/region/{IdRegion}/comuna", async (int IdRegion, ComunaSP comunaSP) =>
            {
                var comunas = await comunaSP.GetComunasByRegionAsync(IdRegion);
                if (comunas == null || comunas.Count == 0)
                {
                    return Results.NotFound($"No se encontr� las comunas en la regi�n {IdRegion}");
                }
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
                //    // Crear nueva comuna (deber�a implementarse un m�todo InsertComunaAsync en ComunaSP)
                //    // Aqu� se asume que existe dicho m�todo, si no, debe implementarse.
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
                    operation.Summary = "No implementado: Crea o actualiza la informaci�n de una comuna.";
                    operation.Description = "Este endpoint a�n no est� implementado.";
                    operation.Responses["501"] = new() { Description = "No implementado." };
                    return operation;
                });
        //})
        //    .WithName("UpsertComuna")
        //    .WithOpenApi(operation =>
        //    {
        //        operation.Summary = "Crea o actualiza la informaci�n de una comuna.";
        //        operation.Description = "Permite crear una nueva comuna o actualizar los datos de una existente en una regi�n.";
        //        operation.RequestBody.Description = "Los datos de la comuna a crear o modificar.";
        //        operation.Responses["200"] = new() { Description = "Comuna creada o actualizada exitosamente." };
        //        operation.Responses["400"] = new() { Description = "No se pudo crear o actualizar la comuna." };
        //        return operation;
        //    });
        }
    }
}
