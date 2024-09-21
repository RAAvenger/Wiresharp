using Microsoft.CodeAnalysis;
using Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Dtos;
using Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Scanning.Abstraction;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Scanning;

internal sealed class GetDependencyInstallerInfoQuery : IGetDependencyInstallerInfoQuery
{
    public DependencyInstallerInfo? RunQuery(SemanticModel semanticModel, SyntaxNode classDeclarationSyntax)
    {
        if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
        {
            // It should not happen in any normal circumstance
            return null;
        }

        return new()
        {
            FullName = $"{classSymbol.ContainingNamespace}.{classSymbol.Name}"
        };
    }
}