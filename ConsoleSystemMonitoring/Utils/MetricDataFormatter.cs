using System.Text;
using ConsoleSystemMonitoring.MetricCollectors.Dto;

namespace ConsoleSystemMonitoring.Utils
{
    public static class MetricDataFormatter
    {
        public static string Format(CpuDto cpuDto, bool useTimestamp)
        {
            var resultString = new StringBuilder();

            resultString.AppendLineWithTimestamp($"Total CPU Usage: {cpuDto.TotalUsagePercent:F2}%", useTimestamp);

            for (int i = 0; i < cpuDto.UsagePerCorePercent.Length; i++)
            {
                resultString.AppendLineWithTimestamp($"Core {i} Usage: {cpuDto.UsagePerCorePercent[i]:F2}%",
                    useTimestamp);
            }

            return resultString.ToString();
        }

        public static string Format(NetworkDto networkDto, bool useTimestamp)
        {
            var resultString = new StringBuilder();

            resultString.AppendLineWithTimestamp(
                $"Total network speed: In = {networkDto.SpeedIn} B/s, Out = {networkDto.SpeedOut} B/s", useTimestamp);

            return resultString.ToString();
        }

        public static string Format(RamDto ramDto, bool useTimestamp)
        {
            var result = new StringBuilder();

            result.AppendLineWithTimestamp($"Total RAM: {ramDto.TotalRamBytes / 1024 / 1024} MB", useTimestamp);
            result.AppendLineWithTimestamp($"Available RAM: {ramDto.AvailableRamBytes / 1024 / 1024} MB", useTimestamp);
            result.AppendLineWithTimestamp($"RAM Usage: {ramDto.RamUsagePercent:F2}%", useTimestamp);

            return result.ToString();
        }
    }
}
