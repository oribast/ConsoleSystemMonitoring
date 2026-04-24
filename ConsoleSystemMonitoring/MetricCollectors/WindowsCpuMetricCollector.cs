using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    [SupportedOSPlatform("windows")]
    internal class WindowsCpuMetricCollector : BaseMetricCollector, IDisposable
    {
        private readonly PerformanceCounter _totalCounter;
        private readonly PerformanceCounter[] _coreCounters;

        public WindowsCpuMetricCollector(Configuration config) : base(config)
        {
            _totalCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");

            _coreCounters = new PerformanceCounter[Environment.ProcessorCount];
            for (int i = 0; i < _coreCounters.Length; i++)
            {
                _coreCounters[i] = new PerformanceCounter("Processor", "% Processor Time", i.ToString());
            }

            //_totalCounter.NextValue();
            //foreach (var coreCounter in _coreCounters)
            //{
            //    coreCounter.NextValue();
            //}
        }

        public override string GetStringData()
        {
            var resultString = new StringBuilder();

            AppendCurrentDateTimeToLogFileData(resultString);
            resultString.AppendLine($"Total CPU Usage: {GetTotalCpuUsage():F2}%");

            var usagePerCore = GetCpuUsagePerCore();
            for (int i = 0; i < usagePerCore.Length; i++)
            {
                AppendCurrentDateTimeToLogFileData(resultString);
                resultString.AppendLine($"Core {i} Usage: {usagePerCore[i]:F2}%");
            }
            return resultString.ToString();
        }

        private double GetTotalCpuUsage() => _totalCounter.NextValue();

        private double[] GetCpuUsagePerCore()
        {
            double[] usagePerCore = new double[_coreCounters.Length];
            for (int i = 0; i < _coreCounters.Length; i++)
            {
                usagePerCore[i] = _coreCounters[i].NextValue();
            }

            return usagePerCore;
        }

        public void Dispose()
        {
            _totalCounter.Dispose();

            foreach (var coreCounter in _coreCounters)
            {
                coreCounter.Dispose();
            }
        }
    }
}
