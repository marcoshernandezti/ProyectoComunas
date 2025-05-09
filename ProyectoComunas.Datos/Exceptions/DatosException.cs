using System;

namespace ProyectoComunas.Datos.Exceptions
{
    public class DatosException : Exception
    {
        public DatosException(string message) : base(message) { }

        public DatosException(string message, Exception innerException) : base(message, innerException) { }
    }
}
