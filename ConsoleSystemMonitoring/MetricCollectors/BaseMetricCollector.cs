namespace ConsoleSystemMonitoring.MetricCollectors
{
    internal abstract class BaseMetricCollector<T>
    {
        public abstract T GetMetricData();
    }
}
