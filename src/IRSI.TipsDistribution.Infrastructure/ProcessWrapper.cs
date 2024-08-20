using System.Diagnostics;
using IRSI.TipsDistribution.Application.Contracts;

namespace IRSI.TipsDistribution.Infrastructure;

public class ProcessWrapper : IProcess
{
    public Process? Start(ProcessStartInfo startInfo)
    {
        return Process.Start(startInfo);
    }

    public Process? Start(string applicationName, string arguments)
    {
        return Process.Start(applicationName, arguments);
    }
}