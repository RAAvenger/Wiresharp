using Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates.Commons;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates;

internal static class TemplateDependencyInjectionAttribute
{
    public const string Code = $$"""
        namespace {{CommonCodes.Namespace}}
        {
            [System.AttributeUsage(System.AttributeTargets.Class)]
            internal sealed class {{Name}} : System.Attribute
            {
            }
        }
        """;

    public const string Name = "InjectDependencyAttribute";
    public static string FullName => $"{CommonCodes.Namespace}.{Name}";
}