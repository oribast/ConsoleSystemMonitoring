using System.Text;
using Hardware.Info;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    internal class WindowsRamMetricCollector : BaseMetricCollector
    {
        private readonly HardwareInfo _info = new();

        public override string GetStringData()
        {
            _info.RefreshMemoryStatus();
            var resultString = new StringBuilder();

            resultString.AppendLine($"Total RAM: {GetTotalRamValue() / 1024 / 1024} MB");
            resultString.AppendLine($"Available RAM: {GetAvailableRamValue() / 1024 / 1024} MB");
            resultString.AppendLine($"RAM Usage: {GetRamUsagePercent():F2}%");

            return resultString.ToString();
        }

        private ulong GetTotalRamValue() => _info.MemoryStatus.TotalPhysical;
        private ulong GetAvailableRamValue() => _info.MemoryStatus.AvailablePhysical;
        private double GetRamUsagePercent() => (double)(GetTotalRamValue() - GetAvailableRamValue()) / GetTotalRamValue() * 100;
    }
}
