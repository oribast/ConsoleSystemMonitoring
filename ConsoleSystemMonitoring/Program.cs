using System.Text.Json;
using ConsoleSystemMonitoring.MetricCollectors;
using ConsoleSystemMonitoring.Utils;

namespace ConsoleSystemMonitoring
{
    internal class Program
    {
        /* По идее должен быть на том же уровне, что и .exe для маленького приложения,
         *  но для удобства запуска из студии установлен такой
         */
        private const string ConfigFilePath = "../../../config.json";

        static void Main(string[] args)
        {
            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine("This application is only supported on Windows.");
                return;
            }

            var config = JsonSerializer.Deserialize<Configuration>(File.ReadAllText(ConfigFilePath))
                         ?? new Configuration { CollectionInterval = 1, OutputFormat = OutputFormat.Console };

            StartUp(config);
        }

        private static void StartUp(Configuration config)
        {
            var cpuCollector = new WindowsCpuMetricCollector();
            var ramCollector = new WindowsRamMetricCollector();
            var netCollector = new WindowsNetworkMetricCollector();

            while (true)
            {
                if (config.OutputFormat == OutputFormat.Console)
                {
                    ConsoleOutput(config, cpuCollector, ramCollector, netCollector);
                }
                else if (config.LogFileName != null)
                {
                    FileOutput(config, cpuCollector, ramCollector, netCollector);
                }
            }
        }

        private static void ConsoleOutput(Configuration config,
            WindowsCpuMetricCollector cpuCollector,
            WindowsRamMetricCollector ramCollector,
            WindowsNetworkMetricCollector netCollector)
        {
            Console.WriteLine(MetricDataFormatter.Format(cpuCollector.GetMetricData(), config.UseTimestamp));
            Console.WriteLine(MetricDataFormatter.Format(ramCollector.GetMetricData(), config.UseTimestamp));
            Console.WriteLine(MetricDataFormatter.Format(netCollector.GetMetricData(), config.UseTimestamp));
            Thread.Sleep(config.CollectionInterval * 1000);
            Console.Clear();
        }

        private static void FileOutput(Configuration config,
            WindowsCpuMetricCollector cpuCollector,
            WindowsRamMetricCollector ramCollector,
            WindowsNetworkMetricCollector netCollector)
        {
            File.AppendAllText(config.LogFileName, 
                MetricDataFormatter.Format(cpuCollector.GetMetricData(), config.UseTimestamp));
            File.AppendAllText(config.LogFileName, 
                MetricDataFormatter.Format(ramCollector.GetMetricData(), config.UseTimestamp));
            File.AppendAllText(config.LogFileName, 
                MetricDataFormatter.Format(netCollector.GetMetricData(), config.UseTimestamp));
            Thread.Sleep(config.CollectionInterval * 1000);
        }
    }
}
