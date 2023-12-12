#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class BillEscrow
    {
        public int Id { get; set; }
        public int Bill { get; set; }
        public int Escrow { get; set; }
    }
}
