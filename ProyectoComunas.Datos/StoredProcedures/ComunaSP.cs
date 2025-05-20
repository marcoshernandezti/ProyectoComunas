using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProyectoComunas.Datos.Exceptions;
using ProyectoComunas.Datos.Models;

namespace ProyectoComunas.Datos.StoredProcedures
{
    public class ComunaSP
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ComunaSP> _logger;

        public ComunaSP(ApplicationDbContext context, ILogger<ComunaSP> logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<List<Comuna>> GetComunasByRegionAsync(int idRegion)
        {
            var comunas = new List<Comuna>
            {
                new Comuna { IdComuna = 1, IdRegion = 1, NombreComuna = "Valpar@acute so", InformacionAdicional = "Puerto principal de Chile" },
                new Comuna { IdComuna = 2, IdRegion = 1, NombreComuna = "Vi@acute a del Mar", InformacionAdicional = "Conocida como la Ciudad Jard@acute in" }
            };
            return await Task.FromResult(comunas);
            //try
            //{
            //    _logger.LogDebug("Obteniendo lista de comunas, idRegion = {idRegion}", idRegion);
            //    var result = await _context.Comunas.FromSqlRaw("EXEC pa_obtener_comunas_por_region @IdRegion = {0}", idRegion).ToListAsync();
            //    // Reemplazar acentos en los nombres de comuna
            //    foreach (var c in result)
            //    {
            //        if (!string.IsNullOrEmpty(c.NombreComuna))
            //            c.NombreComuna = c.NombreComuna.Replace("í", "@acute");
            //    }
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error al obtener la lista de comunas por región, idRegion = {idRegion}", idRegion);
            //    throw new DatosException("Error al obtener la lista de comunas por región", ex);
            //}
        }

        public async Task<Comuna?> GetComunaByIdAsync(int idRegion, int idComuna)
        {
            var comuna = new Comuna
            {
                IdComuna = 1,
                IdRegion = 1,
                NombreComuna = $"Valpara{(char)237}so",
                InformacionAdicional = "<padre><hijo1>uno</hijo1><hijo2>dos</hijo2></padre>",
            };

            return await Task.FromResult(comuna);
            //try
            //{
            //    _logger.LogDebug("Obteniendo datos de la comuna. idRegion = {idRegion}, idComuna = {idComuna}", idRegion, idComuna);
            //    var result = await _context.Comunas.FromSqlRaw("EXEC pa_obtener_comuna @IdRegion = {0}, @IdComuna = {1}", idRegion, idComuna).FirstOrDefaultAsync();
            //    if (result != null && !string.IsNullOrEmpty(result.NombreComuna))
            //        result.NombreComuna = result.NombreComuna.Replace("í", "@acute");
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error al obtener los datos de la comuna. idRegion = {idRegion}, idComuna = {idComuna}", idRegion, idComuna);
            //    throw new DatosException("Error al obtener los datos de la comuna", ex);
            //}
        }

        public async Task<int> UpdateComunaAsync(int idRegion, Comuna comuna)
        {
            if (comuna == null)
            {
                throw new ArgumentNullException(nameof(comuna), "El objeto comuna no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(comuna.NombreComuna))
            {
                throw new ArgumentException("El nombre de la comuna no puede ser nulo o vacío.", nameof(comuna.NombreComuna));
            }

            try
            {
                _logger.LogDebug("Actualizando datos de la comuna. idRegion = {idRegion}, idComuna = {idComuna}", idRegion, comuna.IdComuna);
                return await _context.Database.ExecuteSqlRawAsync(
                    "EXEC pa_actualizar_comuna_comuna @IdRegion = {0}, @IdComuna = {1}, @Comuna = {2}, @InformacionAdicional = {3}",
                    idRegion, comuna.IdComuna, comuna.NombreComuna, comuna.InformacionAdicional ?? string.Empty);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar los datos de la comuna. idRegion = {idRegion}, idComuna = {idComuna}", idRegion, comuna.IdComuna);
                throw new DatosException("Error al actualizar los datos de la comuna", ex);
            }
        }
    }
}
