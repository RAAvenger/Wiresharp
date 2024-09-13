namespace Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates;

internal class DependencyInjectionAttributeCode
{
    public const string Attribute = $$"""
        namespace Wiresharp
        {
            [System.AttributeUsage(System.AttributeTargets.Class)]
            public sealed class {{ClassName}} : System.Attribute
            {
            }
        }
        """;

    public const string ClassName = "InjectDependencyAttribute";
    private const string Namespace = "Wiresharp";
    public static string ClassFullName => $"{Namespace}.{ClassName}";
}