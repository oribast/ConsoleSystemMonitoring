using System.Text;
using Hardware.Info;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    internal class WindowsRamMetricCollector: BaseMetricCollector
    {
        private readonly HardwareInfo info = new();

        public override string GetStringData()
        {
            info.RefreshMemoryStatus();
            var resultString = new StringBuilder();

            resultString.AppendLine($"Total RAM: {GetTotalRamValue() / 1024 / 1024} MB");
            resultString.AppendLine($"Available RAM: {GetAvailableRamValue() / 1024 / 1024} MB");
            resultString.AppendLine($"RAM Usage: {GetRamUsagePercent():F2}%");

            return resultString.ToString();
        }

        private ulong GetTotalRamValue() => info.MemoryStatus.TotalPhysical;
        private ulong GetAvailableRamValue() => info.MemoryStatus.AvailablePhysical;
        private double GetRamUsagePercent() => (double)(GetTotalRamValue() - GetAvailableRamValue()) / GetTotalRamValue() * 100;
    }
}
