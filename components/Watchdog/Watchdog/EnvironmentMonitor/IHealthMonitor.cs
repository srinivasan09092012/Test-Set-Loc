//-----------------------------------------------------------------------------------------
// Violators may be punished to the full extent of the law.
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
//
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//-----------------------------------------------------------------------------------------

using Watchdog.Domain;

namespace Watchdog.Monitor
{
    public interface IHealthMonitor
    {
        ServiceHealthInformation Monitor();
    }
}
