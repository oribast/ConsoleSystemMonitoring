using System.Text;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    internal abstract class BaseMetricCollector(Configuration config)
    {
        public abstract string GetStringData();
        public string GetCurrentDateTime() => $"[{DateTime.UtcNow:dd.MM.yyyy HH:mm:ss:ffff}]";

        public void AppendCurrentDateTimeToLogFileData(StringBuilder str)
        {
            if (config.OutputFormat == OutputFormat.LogFile)
            {
                str.Append(GetCurrentDateTime() + " ");
            }
        }
    }
}
