using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection;

[Generator]
public sealed class CodeGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static ctx => ctx.AddSource($"{DependencyInjectionAttributeCode.Attribute}.g.cs",
            SourceText.From(DependencyInjectionAttributeCode.Attribute, Encoding.UTF8)));

        context.SyntaxProvider
           .ForAttributeWithMetadataName(DependencyInjectionAttributeCode.ClassFullName,
               predicate: static (s, _) => true,
               transform: static (ctx, _) => ctx)
           .Where(static m => true);
    }
}