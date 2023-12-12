namespace Nop.Service.GrupoEstrellaSync.Entities
{
    public partial class ClienteVendedor
    {
        public int CodigoDeCliente { get; set; }
        public int CodigoVendedor { get; set; }
        public byte[] Timestamp { get; set; }
    }
}
