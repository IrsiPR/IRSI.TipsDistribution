using IRSI.TipsDistribution.Application.Distribute;
using MediatR;
using Spectre.Console.Cli;

namespace IRSI.TipsDistribution.Cli.Commands.Distribute;

public sealed class DistributeCreciPayCommandSettings : CommandSettings
{
    [CommandOption("-f|--final")] public bool Final { get; set; }
}

public sealed class DistributeCreciPayCommand(IMediator mediator) : AsyncCommand<DistributeCreciPayCommandSettings>
{
    public override async Task<int> ExecuteAsync(CommandContext context, DistributeCreciPayCommandSettings settings)
    {
        await mediator.Send(new DistributeCreciPayRequest(settings.Final));
        return 0;
    }
}