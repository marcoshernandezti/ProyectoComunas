namespace ProyectoComunas.Web.Models
{
    public class Comuna
    {
        public int idComuna { get; set; }
        public int? idRegion { get; set; }
        public string? nombreComuna { get; set; }
        public string? informacionAdicional { get; set; }
    }
}
