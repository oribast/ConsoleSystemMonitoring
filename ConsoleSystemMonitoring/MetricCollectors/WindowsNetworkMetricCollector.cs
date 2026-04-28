using System.Net.NetworkInformation;
using ConsoleSystemMonitoring.MetricCollectors.Dto;

namespace ConsoleSystemMonitoring.MetricCollectors
{
    internal class WindowsNetworkMetricCollector : BaseMetricCollector<NetworkDto>
    {
        private readonly NetworkInterface[] _interfaces;

        private readonly IPv4InterfaceStatistics[] _previousData;

        public WindowsNetworkMetricCollector()
        {
            _interfaces = NetworkInterface.GetAllNetworkInterfaces()
                .Where(iface =>
                    iface.OperationalStatus == OperationalStatus.Up &&
                    iface.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                    iface.NetworkInterfaceType != NetworkInterfaceType.Tunnel &&
                    iface.NetworkInterfaceType != NetworkInterfaceType.Unknown)
                .ToArray();

            _previousData = _interfaces.Select(iface => iface.GetIPv4Statistics()).ToArray();
        }

        public override NetworkDto GetMetricData()
        {
            var totalSpeed = GetTotalInOutSpeed();

            return new NetworkDto(totalSpeed.inSpeed, totalSpeed.outSpeed);
        }

        private (ulong inSpeed, ulong outSpeed) GetTotalInOutSpeed()
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

            return ((ulong)totalIn, (ulong)totalOut);
        }
    }
}
