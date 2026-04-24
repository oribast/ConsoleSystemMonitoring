namespace ConsoleSystemMonitoring.MetricCollectors
{
    internal abstract class BaseMetricCollector
    {
        // TODO: Подумать о необходимости названия метрики
        // TODO: Подумать о необходимости единиц измерения метрики
        // TODO: Подумать о том, как можно передать данные в исходном формате
        public abstract string GetStringData();
    }
}
