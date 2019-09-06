using Common.Interfaces;
using System.Diagnostics;
using System;

namespace Common.Implementations
{
    public class EventViewerLogger : ILogger
    {
        private string logSource;
        private string logSourceName;
        private EventLog winEvtLog = null;
        private bool isReady = false;

        public EventViewerLogger(string logSourceName, string logSource)
        {
            this.logSourceName = logSourceName;
            this.logSource = logSource;

            this.winEvtLog = new EventLog(this.logSourceName, Environment.MachineName, this.logSource);

            if (!EventLog.SourceExists(this.logSource))
            {
                EventLog.CreateEventSource(this.logSource,this.logSourceName);

                try
                {
                    winEvtLog.WriteEntry("Log settings done, event source created", EventLogEntryType.SuccessAudit,900,1);
                }
                catch
                {
                    EventLog.DeleteEventSource(this.logSource);
                }
            }

            this.isReady = true;
        }

        public void writeEntry(string EntryMsg, LogginSeetings.LevelType LoggingLevel, int EventId, short CategoryId)
        {
            if (this.isReady)
            {
                switch ((byte)LoggingLevel)
                {
                    case 1:
                        winEvtLog.WriteEntry(EntryMsg, EventLogEntryType.Error, EventId, CategoryId);
                        break;
                    case 4:
                        winEvtLog.WriteEntry(EntryMsg, EventLogEntryType.Information, EventId, CategoryId);
                        break;
                    case 8:
                        winEvtLog.WriteEntry(EntryMsg, EventLogEntryType.SuccessAudit, EventId, CategoryId);
                        break;
                    case 2:
                        winEvtLog.WriteEntry(EntryMsg, EventLogEntryType.Warning, EventId, CategoryId);
                        break;
                    case 16:
                        winEvtLog.WriteEntry(EntryMsg, EventLogEntryType.FailureAudit, EventId, CategoryId);
                        break;
                }

                Console.WriteLine(EntryMsg);
            }
        }
    }
}
