namespace Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates;

internal class DependencyInjectionAttributeCode
{
    public const string ClassName = "DependencyInjectionAttribute";
    public const string Attribute = $$"""
        namespace Wiresharp
        {
            [System.AttributeUsage(System.AttributeTargets.Class)]
            public sealed class {{ClassName}} : System.Attribute
            {
            }
        }
        """;
}