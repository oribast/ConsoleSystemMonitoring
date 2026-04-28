namespace ConsoleSystemMonitoring.MetricCollectors.Dto
{
    public record CpuDto(
        double TotalUsagePercent,
        double[] UsagePerCorePercent
    );
}
