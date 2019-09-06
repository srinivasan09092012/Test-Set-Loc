namespace Common.Interfaces
{
    public static class LogginSeetings
    {
        public enum LevelType : byte
        {
            InformationApplication = 4,
            ErrorApplication = 1,
            SuccessAudit = 8,
            WarningApplication = 2,
            FailureAudit = 16
        }
    }
}
