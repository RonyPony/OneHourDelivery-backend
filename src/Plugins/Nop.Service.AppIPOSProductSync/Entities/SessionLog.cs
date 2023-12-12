using System;

#nullable disable

namespace Nop.Service.AppIPOSSync.Entities
{
    public partial class SessionLog
    {
        public int Id { get; set; }
        public string Entry { get; set; }
        public string UserName { get; set; }
        public string Comment { get; set; }
        public string Host { get; set; }
        public bool? Manager { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public string Activity { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
