//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of DXC Technology, Copyright (c) 2020. All rights reserved.
//-----------------------------------------------------------------------------------------

namespace Watchdog.Domain
{
    public class UXApplicationConfig : ServiceConfigMetaData
    {
        public string URLValue { get; set; }

        public string Healthurl { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string LoggedInUsername { get; set; }

        public int SleepInterval { get; set; }

        public string SiteName { get; set; }
    }
}