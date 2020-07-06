namespace Common.Interfaces
{
    public interface ILogger
    {
        void writeEntry(string EntryMsg, LogginSeetings.LevelType LoggingLevel, int EventId, short CategoryId);

        void writeEntry(string EntryMsg);
    }
}
