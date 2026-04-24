using System.Net.NetworkInformation;
using System.Text;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    internal class WindowsNetworkMetricCollector : BaseMetricCollector
    {
        private readonly NetworkInterface[] _interfaces;

        private readonly IPv4InterfaceStatistics[] _previousData;

        public WindowsNetworkMetricCollector(Configuration config) : base(config)
        {
            _interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(iface => iface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                                iface.OperationalStatus == OperationalStatus.Up).ToArray();
            _previousData = _interfaces.Select(iface => iface.GetIPv4Statistics()).ToArray();
        }

        public override string GetStringData()
        {
            var resultString = new StringBuilder();
            var data = GetTotalInOutSpeed();

            AppendCurrentDateTimeToLogFileData(resultString);
            resultString.AppendLine($"Total speed: In = {data.Item1} B/s, Out = {data.Item2} B/s");

            return resultString.ToString();
        }

        private (long, long) GetTotalInOutSpeed()
        {
            long totalIn = 0;
            long totalOut = 0;

            for (int i = 0; i < _interfaces.Length; i++)
            {
                var current = _interfaces[i].GetIPv4Statistics();

                totalIn += current.BytesReceived - _previousData[i].BytesReceived;
                totalOut += current.BytesSent - _previousData[i].BytesSent;

                _previousData[i] = current;
            }

            return (totalIn, totalOut);
        }
    }
}
