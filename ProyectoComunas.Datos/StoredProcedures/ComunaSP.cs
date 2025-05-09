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
            try
            {
                _logger.LogDebug("Obteniendo lista de comunas, idRegion = {idRegion}", idRegion);
                return await _context.Comunas.FromSqlRaw("EXEC pa_obtener_comunas_por_region @IdRegion = {0}", idRegion).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de comunas por región, idRegion = {idRegion}", idRegion);
                throw new DatosException("Error al obtener la lista de comunas por región", ex);
            }
        }

        public async Task<Comuna?> GetComunaByIdAsync(int idRegion, int idComuna)
        {
            try
            {
                _logger.LogDebug("Obteniendo datos de la comuna. idRegion = {idRegion}, idComuna = {idComuna}", idRegion, idComuna);
                return await _context.Comunas.FromSqlRaw("EXEC pa_obtener_comuna @IdRegion = {0}, @IdComuna = {1}", idRegion, idComuna).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los datos de la comuna. idRegion = {idRegion}, idComuna = {idComuna}", idRegion, idComuna);
                throw new DatosException("Error al obtener los datos de la comuna", ex);
            }
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
