using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Win32.TaskScheduler;
using Task = System.Threading.Tasks.Task;
using TaskSettings = IRSI.TipsDistribution.Application.Options.TaskSettings;

namespace IRSI.TipsDistribution.Application.Tasks;

public class InstallTaskSchedulerTasksRequestHandler : IRequestHandler<InstallTaskSchedulerTasksRequest>
{
    private readonly IOptions<TaskSettings> _options;
    private readonly ILogger<InstallTaskSchedulerTasksRequestHandler> _logger;

    public InstallTaskSchedulerTasksRequestHandler(IOptions<TaskSettings> options,
        ILogger<InstallTaskSchedulerTasksRequestHandler> logger)
    {
        _options = options;
        _logger = logger;
    }

    private const string APP_NAME = "IRSI.TipsDistribution";

    public Task Handle(InstallTaskSchedulerTasksRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Installing task scheduler tasks");

        var folder = TaskService.Instance.GetFolder(APP_NAME) ??
                     TaskService.Instance.RootFolder.CreateFolder(APP_NAME);

        foreach (var task in folder.GetTasks())
        {
            folder.DeleteTask(task.Name);
        }

        using var process = Process.GetCurrentProcess();
        var path = Path.GetDirectoryName(process.MainModule!.FileName);
        var exeName = Path.GetFileName(process.MainModule.FileName);

        foreach (var scheduledTask in _options.Value.Tasks)
        {
            var newTask = TaskService.Instance.NewTask();
            newTask.RegistrationInfo.Author = "Nelson Segarra";
            newTask.RegistrationInfo.Description = scheduledTask.Description;
            newTask.Actions.Add(new ExecAction(exeName, scheduledTask.Arguments, path));

            if (scheduledTask is { Interval: { } interval, Duration: { } duration })
            {
                var trigger = new DailyTrigger()
                {
                    StartBoundary = DateTime.Today + scheduledTask.StartTime.ToTimeSpan()
                };

                trigger.Repetition.Interval = TimeSpan.FromMinutes(interval);
                trigger.Repetition.Duration = TimeSpan.FromHours(duration);

                newTask.Triggers.Add(trigger);
            }
            else
            {
                var trigger = new DailyTrigger()
                {
                    StartBoundary = DateTime.Today + scheduledTask.StartTime.ToTimeSpan()
                };
                newTask.Triggers.Add(trigger);
            }


            folder.RegisterTaskDefinition(scheduledTask.TaskName, newTask, TaskCreation.CreateOrUpdate,
                _options.Value.Username, _options.Value.Password, TaskLogonType.Password);
        }

        return Task.CompletedTask;
    }
}