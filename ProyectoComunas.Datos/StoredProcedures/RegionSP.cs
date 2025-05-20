using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using ProyectoComunas.Datos.Exceptions;
using ProyectoComunas.Datos.Models;

namespace ProyectoComunas.Datos.StoredProcedures
{
    public class RegionSP
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RegionSP> _logger;

        public RegionSP(ApplicationDbContext context, ILogger<RegionSP> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Region>> GetRegionesAsync()
        {
            var regiones = new List<Region>
            {
                new Region { IdRegion = 1, NombreRegion = "Región de Valparaíso" },
                new Region { IdRegion = 2, NombreRegion = "Región Metropolitana" }
            };
            return await Task.FromResult(regiones);

            //try
            //{
            //    _logger.LogInformation("Obteniendo lista de regiones");
            //    return await _context.Regiones.FromSqlRaw("EXEC pa_obtener_regiones").ToListAsync();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error al obtener la lista de regiones");
            //    throw new DatosException("Error al obtener la lista de regiones", ex);
            //}
        }

        public async Task<Region?> GetRegionByIdAsync(int idRegion)
        {
            if (idRegion != 1) {
                return null;
            }

            var region = new Region
            {
                IdRegion = 1,
                NombreRegion = "Región de Valparaíso"
            };
            return await Task.FromResult(region);

            //try
            //{
            //    _logger.LogDebug("Obteniendo region {idRegion}", idRegion);
            //    return await _context.Regiones.FromSqlRaw("EXEC pa_obtener_region @IdRegion = {0}", idRegion).FirstOrDefaultAsync();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error al obtener datos de la región. IdRegion = {idRegion}", idRegion);
            //    throw new DatosException("Error al obtener datos de la región", ex);
            //}
        }
    }
}
