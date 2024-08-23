using IRSI.TipsDistribution.Application.Tasks;
using MediatR;
using Spectre.Console.Cli;

namespace IRSI.TipsDistribution.Cli.Commands.Tasks;

public class UnInstallTaskSchedulerTasksCommandSettings : CommandSettings
{
}

public class UnInstallTaskSchedulerTasksCommand : AsyncCommand<UnInstallTaskSchedulerTasksCommandSettings>
{
    private readonly IMediator _mediator;

    public UnInstallTaskSchedulerTasksCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, UnInstallTaskSchedulerTasksCommandSettings settings)
    {
        await _mediator.Send(new UnInstallTaskSchedulerTasksRequest());
        return 0;
    }
}