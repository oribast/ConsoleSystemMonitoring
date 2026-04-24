using System.Runtime.InteropServices;

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

            var cpuCollector = new MetricCollectors.WindowsCpuMetricCollector();
            var ramCollector = new MetricCollectors.WindowsRamMetricCollector();
            var netCollector = new MetricCollectors.WindowsNetworkMetricCollector();
            while (true)
            {
                Console.WriteLine(cpuCollector.GetStringData());
                Console.WriteLine(ramCollector.GetStringData());
                Console.WriteLine(netCollector.GetStringData());
                Thread.Sleep(1000);
                Console.Clear();
            }
        }
    }
}
