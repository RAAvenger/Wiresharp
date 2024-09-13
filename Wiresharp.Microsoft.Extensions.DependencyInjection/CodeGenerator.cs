using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection;

[Generator]
public class CodeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource($"{DependencyInjectionAttributeCode.Attribute}.g.cs",
            SourceText.From(DependencyInjectionAttributeCode.Attribute, Encoding.UTF8)));

        // TODO: implement the remainder of the source generator
    }
}