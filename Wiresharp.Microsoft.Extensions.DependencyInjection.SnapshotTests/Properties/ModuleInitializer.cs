using System.Runtime.CompilerServices;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection.SnapshotTests.Properties;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
    }
}