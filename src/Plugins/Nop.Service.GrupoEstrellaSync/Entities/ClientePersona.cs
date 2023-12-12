using System;
using System.Collections.Generic;

#nullable disable

namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class ClientePersona
    {
        public int CodigoDeCliente { get; set; }
        public string RegistroId { get; set; }
        public string NumeroId { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public short? NumeroDeHijos { get; set; }
        public string DescripcionTarjeta { get; set; }
        public string NumeroTarjeta { get; set; }
        public string TipoDeVivienda { get; set; }
        public string CodigoProfesion { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
