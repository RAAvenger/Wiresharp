using Microsoft.CodeAnalysis;
using Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Dtos;
using Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Scanning.Abstraction;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Scanning;

internal sealed class GetDependencyInstallerInfoQuery : IGetDependencyInstallerInfoQuery
{
    public DependencyInstallerInfo RunQuery(SemanticModel semanticModel, SyntaxNode classDeclarationSyntax)
    {
        // Get the semantic representation of the enum syntax
        if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol enumSymbol)
        {
            // something went wrong
        }
        throw new NotImplementedException();
    }
}