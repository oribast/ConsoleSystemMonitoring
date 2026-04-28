namespace ConsoleSystemMonitoring.MetricCollectors.Dto
{
    public record RamDto(
        ulong TotalRamBytes,
        ulong AvailableRamBytes,
        double RamUsagePercent
    );
}
