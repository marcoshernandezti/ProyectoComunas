namespace ProyectoComunas.Datos.Models
{
    public class Comuna
    {
        public int IdComuna { get; set; }
        public int? IdRegion { get; set; }
        public string? NombreComuna { get; set; }
        public string? InformacionAdicional { get; set; }
    }
}
