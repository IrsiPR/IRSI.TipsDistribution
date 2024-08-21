using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Spectre.Console.Cli;

namespace IRSI.TipsDistribution.Cli;

public class TypeRegistrar(IHostBuilder builder) : ITypeRegistrar
{
    public void Register(Type service, Type implementation)
    {
        builder.ConfigureServices(services => services.AddSingleton(service, implementation));
    }

    public void RegisterInstance(Type service, object implementation)
    {
        builder.ConfigureServices(services => services.AddSingleton(service, implementation));
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        if (factory is null) throw new ArgumentNullException(nameof(factory));
        builder.ConfigureServices(services => services.AddSingleton(service, _ => factory()));
    }

    public ITypeResolver Build()
    {
        return new TypeResolver(builder.Build());
    }
}