using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Win32.TaskScheduler;
using Task = System.Threading.Tasks.Task;

namespace IRSI.TipsDistribution.Application.Tasks;

public class UnInstallTaskSchedulerTasksRequestHandler : IRequestHandler<UnInstallTaskSchedulerTasksRequest>
{
    private readonly ILogger<UnInstallTaskSchedulerTasksRequestHandler> _logger;

    public UnInstallTaskSchedulerTasksRequestHandler(ILogger<UnInstallTaskSchedulerTasksRequestHandler> logger)
    {
        _logger = logger;
    }

    private const string APP_NAME = "IRSI.TipsDistribution";

    public Task Handle(UnInstallTaskSchedulerTasksRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UnInstalling task scheduler tasks");

        var folder = TaskService.Instance.GetFolder(APP_NAME) ??
                     TaskService.Instance.RootFolder.CreateFolder(APP_NAME);

        foreach (var task in folder.GetTasks())
        {
            folder.DeleteTask(task.Name);
        }

        TaskService.Instance.RootFolder.DeleteFolder(APP_NAME);

        return Task.CompletedTask;
    }
}