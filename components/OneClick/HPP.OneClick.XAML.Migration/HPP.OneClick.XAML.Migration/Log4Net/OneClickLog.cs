using log4net;
using System;
using System.IO;

namespace HPP.OneClick.XAML.Migration.Log4Net
{
    public class OneClickLog : IOneClickLog
    {
        private readonly ILog log;

        static OneClickLog()
        {
            var log4NetConfigDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            var log4NetConfigFilePath = Path.Combine(log4NetConfigDirectory, "log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));

            if (!File.Exists(log4NetConfigFilePath))
            {
                log4NetConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs\\log4net.config");
            }

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(log4NetConfigFilePath));

        }

        public OneClickLog(Type t)
        {
            this.log = LogManager.GetLogger(t);
        }

        public void Info(object msg)
        {
            this.log.Info(msg);
        }

        public void Warning(object msg)
        {
            this.log.Warn(msg);
        }

        public void Error(object msg)
        {
            this.log.Error(msg);
        }

        public void Debug(object msg)
        {
            this.log.Debug(msg);
        }
    }
}
