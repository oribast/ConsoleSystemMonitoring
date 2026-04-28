using System.Diagnostics;
using System.Runtime.Versioning;
using ConsoleSystemMonitoring.MetricCollectors.Dto;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    [SupportedOSPlatform("windows")]
    internal class WindowsCpuMetricCollector : BaseMetricCollector<CpuDto>, IDisposable
    {
        private readonly PerformanceCounter _totalCounter;
        private readonly PerformanceCounter[] _coreCounters;

        public WindowsCpuMetricCollector()
        {
            _totalCounter = new PerformanceCounter("Processor", 
                "% Processor Time", "_Total");

            _coreCounters = new PerformanceCounter[Environment.ProcessorCount];
            for (int i = 0; i < _coreCounters.Length; i++)
            {
                _coreCounters[i] = new PerformanceCounter("Processor", 
                    "% Processor Time", i.ToString());
            }

            /*
             * Начальная инициализация счетчиков, чтобы при первом вызове NextValue() возвращалось корректное значение
             */
            _totalCounter.NextValue();
            foreach (var coreCounter in _coreCounters)
            {
                coreCounter.NextValue();
            }
        }

        public override CpuDto GetMetricData()
            => new CpuDto(GetTotalCpuUsage(), GetCpuUsagePerCore());

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
