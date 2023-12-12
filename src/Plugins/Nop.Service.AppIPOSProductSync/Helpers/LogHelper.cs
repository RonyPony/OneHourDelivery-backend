using System.Diagnostics;

namespace Nop.Service.AppIPOSSync.Helpers
{
    /// <summary>
    /// Class used to log information to windows event viewer
    /// </summary>
    public class LogHelper
    {
        private readonly string _eventViewerName;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="eventViewerName">Name of the event viewer that messages will be logged to</param>
        public LogHelper(string eventViewerName)
        {
            _eventViewerName = eventViewerName;
        }

        /// <summary>
        /// Logs an entry to the event viewer
        /// </summary>
        /// <param name="message">Message to be logged</param>
        /// <param name="type">Type of the event that will be logged</param>
        public void LogEventViewer(string message, EventLogEntryType type)
        {
            string logName = "Application";

            if(!EventLog.SourceExists(_eventViewerName))
                EventLog.CreateEventSource(_eventViewerName, logName);

            using EventLog eventLog = new EventLog(logName)
            {
                Source = _eventViewerName
            };

            eventLog.WriteEntry(message, type, 101);
        }
    }
}
