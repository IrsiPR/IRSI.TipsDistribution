using IRSI.TipsDistribution.Application.Tasks;
using MediatR;
using Spectre.Console.Cli;

namespace IRSI.TipsDistribution.Cli.Commands.Tasks;

public class InstallTaskSchedulerTasksCommand(IMediator mediator) : AsyncCommand
{
    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        await mediator.Send(new InstallTaskSchedulerTasksRequest());
        return 0;
    }
}