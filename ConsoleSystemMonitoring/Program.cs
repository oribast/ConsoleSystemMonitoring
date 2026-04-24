using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

namespace ConsoleSystemMonitoring
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine("This application is only supported on Windows.");
                return;
            }

            var config = JsonSerializer.Deserialize<Configuration>(File.ReadAllText("../../../config.json"))
                         ?? new Configuration { CollectionInterval = 1, OutputFormat = OutputFormat.Console };
            
            var cpuCollector = new MetricCollectors.WindowsCpuMetricCollector(config);
            var ramCollector = new MetricCollectors.WindowsRamMetricCollector(config);
            var netCollector = new MetricCollectors.WindowsNetworkMetricCollector(config);
            while (true)
            {
                if (config.OutputFormat == OutputFormat.Console)
                {
                    Console.WriteLine(cpuCollector.GetStringData());
                    Console.WriteLine(ramCollector.GetStringData());
                    Console.WriteLine(netCollector.GetStringData());
                    Thread.Sleep(config.CollectionInterval * 1000);
                    Console.Clear();
                }
                else if (config.LogFileName != null)
                {
                    File.AppendAllText(config.LogFileName, cpuCollector.GetStringData() + Environment.NewLine);
                    File.AppendAllText(config.LogFileName, ramCollector.GetStringData() + Environment.NewLine);
                    File.AppendAllText(config.LogFileName, netCollector.GetStringData() + Environment.NewLine);
                    Thread.Sleep(config.CollectionInterval * 1000);
                }
            }
        }
    }
}
