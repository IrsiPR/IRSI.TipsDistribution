using IRSI.TipsDistribution.Application.Tasks;
using MediatR;
using Spectre.Console.Cli;

namespace IRSI.TipsDistribution.Cli.Commands.Tasks;

public class UnInstallTaskSchedulerTasksCommand(IMediator mediator) : AsyncCommand
{
    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        await mediator.Send(new UnInstallTaskSchedulerTasksRequest());
        return 0;
    }
}