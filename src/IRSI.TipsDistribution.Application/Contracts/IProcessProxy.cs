using System.Diagnostics;

namespace IRSI.TipsDistribution.Application.Contracts;

public interface IProcess
{
    Process? Start(ProcessStartInfo startInfo);
}