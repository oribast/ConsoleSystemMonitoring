using System.Text;
using Hardware.Info;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    internal class WindowsRamMetricCollector(Configuration config) : BaseMetricCollector(config)
    {
        private readonly HardwareInfo _info = new();

        public override string GetStringData()
        {
            _info.RefreshMemoryStatus();
            var resultString = new StringBuilder();

            // TODO: Подумать о методе расширении для добавления текущей даты и времени в строку, чтобы не дублировать код
            AppendCurrentDateTimeToLogFileData(resultString);
            resultString.AppendLine($"Total RAM: {GetTotalRamValue() / 1024 / 1024} MB");
            AppendCurrentDateTimeToLogFileData(resultString);
            resultString.AppendLine($"Available RAM: {GetAvailableRamValue() / 1024 / 1024} MB");
            AppendCurrentDateTimeToLogFileData(resultString);
            resultString.AppendLine($"RAM Usage: {GetRamUsagePercent():F2}%");

            return resultString.ToString();
        }

        private ulong GetTotalRamValue() => _info.MemoryStatus.TotalPhysical;
        private ulong GetAvailableRamValue() => _info.MemoryStatus.AvailablePhysical;
        private double GetRamUsagePercent() => (double)(GetTotalRamValue() - GetAvailableRamValue()) / GetTotalRamValue() * 100;
    }
}
