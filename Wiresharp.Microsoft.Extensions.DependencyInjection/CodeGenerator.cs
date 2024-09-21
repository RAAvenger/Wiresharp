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
        context.RegisterPostInitializationOutput(static ctx => ctx.AddSource($"{TemplateDependencyInjectionAttribute.Name}.g.cs",
            SourceText.From(TemplateDependencyInjectionAttribute.Code, Encoding.UTF8)));
        context.RegisterPostInitializationOutput(static ctx => ctx.AddSource($"{TemplateIDependencyInstaller.Name}.g.cs",
            SourceText.From(TemplateIDependencyInstaller.Code, Encoding.UTF8)));

        context.SyntaxProvider
           .ForAttributeWithMetadataName(TemplateDependencyInjectionAttribute.FullName,
               predicate: static (s, _) => true,
               transform: static (ctx, _) => ctx)
           .Where(static m => true);
    }
}