//HintName: IDependencyInstaller.g.cs
using Microsoft.Extensions.DependencyInjection;

namespace Wiresharp
{
    internal interface IDependencyInstaller
    {
        public void InstallDependencies(IServiceCollection services);
    }
}