namespace IRSI.TipsDistribution.Application.Options;

public class TaskSettings
{
    public const string SectionName = "TaskOptions";

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public List<ScheduledTask> Tasks { get; set; } = [];
}