using MediatR;

namespace IRSI.TipsDistribution.Application.Distribute;

public record DistributeCreciPayRequest(bool Final) : IRequest;