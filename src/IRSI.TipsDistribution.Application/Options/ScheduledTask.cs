namespace IRSI.TipsDistribution.Application.Options;

public class ScheduledTask
{
    public string AppName { get; set; } = string.Empty;
    public string TaskName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Arguments { get; set; } = string.Empty;
    public TimeOnly StartTime { get; set; }
    public int? Interval { get; set; }
    public int? Duration { get; set; }
}