//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

namespace Watchdog.Domain
{
    public class Constants
    {
        public static class FileName
        {
            public const string WatchdogConfigFile = "WatchdogConfig.xml";                      
        }

        public static class Status
        {
            public const string Running = "Running";
            public const string NotRunning = "NotRunning";
            public const string Failed = "Failed";
            public const string Restarted = "Restarted";
            public const string None = "None";
        }
    }

    public class UXMonitoringConstants
    {
        public const string UserName = "UserName";

        public const string Password = "Password";

        public const string SuccessfullyLoggedInPageTitle = "My Home";

        public const string SuccessfullyLoggedInWelcomeID = "pageBannerWelcome";

        public const string LoginXpath = "//*[@id='AAGMenu.Login']/div[2]/a";

        public const string LoginFailed = "Login failed";

        public const string LoginSuccess = "Login Success";

    }
}