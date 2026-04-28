namespace ConsoleSystemMonitoring.MetricCollectors.Dto
{
    public record NetworkDto(
        ulong SpeedIn,
        ulong SpeedOut
    );
}
