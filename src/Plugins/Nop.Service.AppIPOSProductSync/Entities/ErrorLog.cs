#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class ErrorLog
    {
        public int Id { get; set; }
        public string Station { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorType { get; set; }
        public string Description { get; set; }
    }
}
