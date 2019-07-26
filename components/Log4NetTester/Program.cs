using HPE.HSP.UA3.Core.API.Logger.Loggers;
using System;

namespace Log4NetTester
{
    public class Program
    {
        private static CoreLogger logger = new CoreLogger();

        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Logging to Windows event log");

                Console.WriteLine("Logging DEBUG");
                logger.LogDebug("This is a DEBUG test.");

                Console.WriteLine("Logging ERROR");
                logger.LogError("This is a ERROR test.");

                Console.WriteLine("Logging FATAL");
                logger.LogFatal("This is a FATAL test.");

                Console.WriteLine("Logging INFO");
                logger.LogInformational("This is a INFO test.");

                Console.WriteLine("Logging WARN");
                logger.LogWarning("This is a WARN test.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                Console.Write("Press any key to close...");
                Console.ReadKey();
            }
        }
    }
}
