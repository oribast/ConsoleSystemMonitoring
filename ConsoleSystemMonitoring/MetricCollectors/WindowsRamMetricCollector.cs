using System.Management;
using System.Runtime.Versioning;
using ConsoleSystemMonitoring.MetricCollectors.Dto;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    [SupportedOSPlatform("windows")]
    internal class WindowsRamMetricCollector : BaseMetricCollector<RamDto>
    {
        public override RamDto GetMetricData()
        {
            var total = GetTotalMemoryBytes();
            var free = GetAvailableMemoryBytes();
            var percent = GetRamUsagePercent(total, free);

            return new RamDto(total, free, percent);
        }

        private ulong GetTotalMemoryBytes()
        {
            ulong total = 0;

            using var totalSearcher = new ManagementObjectSearcher(
                "SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");

            foreach (ManagementObject obj in totalSearcher.Get())
            {
                total = (ulong)obj["TotalPhysicalMemory"];
            }

            return total;
        }

        private ulong GetAvailableMemoryBytes()
        {
            ulong free = 0;

            using var freeSearcher = new ManagementObjectSearcher(
                "SELECT FreePhysicalMemory FROM Win32_OperatingSystem");

            foreach (ManagementObject obj in freeSearcher.Get())
            {
                free = (ulong)obj["FreePhysicalMemory"];
            }

            return free * 1024;
        }

        private double GetRamUsagePercent(ulong total, ulong free)
        {
            ulong used = total - free;
            double percent = (double)used / total * 100;

            return percent;
        }
    }
}
