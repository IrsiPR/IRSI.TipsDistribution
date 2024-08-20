using IRSI.CommonTools.Abstractions;
using IRSI.TipsDistribution.Application.Contracts;
using MediatR;

namespace IRSI.TipsDistribution.Application.Distribute;

public class DistributeCreciPayRequestHandler : IRequestHandler<DistributeCreciPayRequest>
{
    private readonly IDatedFoldersService _datedFoldersService;
    private readonly IDateOnly _dateOnlyWrapper;

    public DistributeCreciPayRequestHandler(IDatedFoldersService datedFoldersService, IDateOnly dateOnlyWrapper)
    {
        _datedFoldersService = datedFoldersService;
        _dateOnlyWrapper = dateOnlyWrapper;
    }

    public async Task Handle(DistributeCreciPayRequest request, CancellationToken cancellationToken)
    {
        if (request.Final)
        {
            await DistributeCreciPayFinal();
        }
        else
        {
            await DistributeCreciPayRecurring();
        }
    }

    private async Task DistributeCreciPayFinal()
    {
        var businessDate = _dateOnlyWrapper.FromDateTime(DateTime.Today).AddDays(-1);
        Console.WriteLine("Distributing CreciPay Final for {0}", businessDate);
    }

    private async Task DistributeCreciPayRecurring()
    {
        Console.WriteLine("Distributing CreciPay Recurring");
    }
}