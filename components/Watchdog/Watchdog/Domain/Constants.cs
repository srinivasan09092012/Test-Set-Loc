using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Watchdog.Domain
{
    public class Constants
    {
        public static class FileName
        {
            public const string WatchdogConfigFile = "WatchdogConfig.xml";                      
        }
    }

    public class UXMonitoringConstants
    {
        public const string UserName =
                "UserName";

        public const string Password =
            "Password";

        public const string SuccessfullyLoggedInPageTitle =
            "My Home";

        public const string SuccessfullyLoggedInWelcomeID =
            "pageBannerWelcome";

        public const string LoginXpath =
            "//*[@id='AAGMenu.Login']/div[2]/a";
    }
}
