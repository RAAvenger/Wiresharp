//HintName: IDependencyInstaller.g.cs
namespace Wiresharp
{
    internal interface IDependencyInstaller
    {
        public void InstallDependencies(IServiceCollection services);
    }
}