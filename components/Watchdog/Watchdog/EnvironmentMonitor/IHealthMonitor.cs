using Watchdog.Domain;

namespace Watchdog.Monitor
{
    public interface IHealthMonitor
    {
        ServiceHealthInformation Monitor();
    }
}
