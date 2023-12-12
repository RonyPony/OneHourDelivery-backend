using System.Diagnostics;

namespace Nop.Service.GrupoEstrellaSync.Helper
{
    public class LogHelper
    {
        public enum Logtype
        {
            Error = 1,
            Info = 2
        }


        public static void LogEventViewer(string message, Logtype type)
        {
            using (EventLog eventLog = new EventLog("Application"))
            {
                eventLog.Source = "Nop.Service.GrupoEstrellaSync";

                switch (type)
                {
                    case Logtype.Error:
                        eventLog.WriteEntry(message, EventLogEntryType.Error, 101);
                        break;
                    case Logtype.Info:
                        eventLog.WriteEntry(message, EventLogEntryType.Information, 101);
                        break;
                }
            }
        }
    }
}
