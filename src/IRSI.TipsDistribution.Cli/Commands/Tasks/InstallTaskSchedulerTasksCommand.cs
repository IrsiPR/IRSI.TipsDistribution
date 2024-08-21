using IRSI.TipsDistribution.Application.Tasks;
using MediatR;
using Spectre.Console.Cli;

namespace IRSI.TipsDistribution.Cli.Commands.Tasks;

public class InstallTaskSchedulerTasksCommandSettings : CommandSettings
{
}

public class InstallTaskSchedulerTasksCommand : AsyncCommand<InstallTaskSchedulerTasksCommandSettings>
{
    // private readonly IMediator _mediator;

    // public InstallTaskSchedulerTasksCommand(IMediator mediator)
    // {
    //     _mediator = mediator;
    // }

    public override async Task<int> ExecuteAsync(CommandContext context, InstallTaskSchedulerTasksCommandSettings settings)
    {
        // await _mediator.Send(new InstallTaskSchedulerTasksRequest());
        return 0;
    }
}

