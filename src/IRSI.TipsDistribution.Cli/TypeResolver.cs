using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace IRSI.TipsDistribution.Cli;

public class TypeResolver(IHost host) : ITypeResolver, IDisposable
{
    public object? Resolve(Type? type)
    {
        return type is not null ? host.Services.GetService(type) : null;
    }

    public void Dispose()
    {
        host.Dispose();
    }
}