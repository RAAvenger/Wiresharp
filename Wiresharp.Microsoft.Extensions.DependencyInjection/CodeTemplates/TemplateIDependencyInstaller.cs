using Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates.Commons;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates;

internal static class TemplateIDependencyInstaller
{
    public const string Code = $$"""
        namespace {{CommonCodes.Namespace}}
        {
            internal interface {{Name}}
            {
                public void {{MethodName}}({{FirstParameterType}} {{FirstParameterName}});
            }
        }
        """;

    public const string MethodName = "InstallDependencies";
    public const string Name = "IDependencyInstaller";
    public const string FirstParameterName = "services";
    public const string FirstParameterType = "IServiceCollection";
    public static string FullName => $"{CommonCodes.Namespace}.{Name}";
}