using Microsoft.CodeAnalysis;
using Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Dtos;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Scanning.Abstraction;
internal interface IGetDependencyInstallerInfoQuery
{
    DependencyInstallerInfo RunQuery(SemanticModel semanticModel, SyntaxNode enumDeclarationSyntax);
}