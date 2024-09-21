using FluentAssertions;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates;
using Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Dtos;
using Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Scanning;
using Wiresharp.Microsoft.Extensions.DependencyInjection.UseCases.Scanning.Abstraction;
using Xunit;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection.UnitTests.UseCases.Scanning;

public class GetDependencyInstallerInfoQueryTests
{
    private readonly IGetDependencyInstallerInfoQuery _sut;

    public GetDependencyInstallerInfoQueryTests()
    {
        _sut = new GetDependencyInstallerInfoQuery();
    }

    [Fact]
    public void RunQuery_ShouldReturnInfoContainingClassFullName_WhenClassDeclarationSyntaxIsNotAssociatedWithANamedTypeSymbol()
    {
        // Arrange
        var namespaceName = "DummyNamespace";
        var className = "DummyInstaller";
        var classDeclaration = SyntaxFactory.ClassDeclaration(className)
            .WithAttributeLists([
                SyntaxFactory.AttributeList([
                    SyntaxFactory.Attribute(
                        SyntaxFactory.ParseName(TemplateDependencyInjectionAttribute.FullName))])])
            .WithModifiers(
                SyntaxFactory.TokenList(
                    SyntaxFactory.Token(SyntaxKind.InternalKeyword)))
            .WithBaseList(
                SyntaxFactory.BaseList([
                    SyntaxFactory.SimpleBaseType(
                        SyntaxFactory.IdentifierName(TemplateIDependencyInstaller.Name))]))
            .WithMembers([
                SyntaxFactory.MethodDeclaration(
                    SyntaxFactory.ParseTypeName("void"),
                        TemplateIDependencyInstaller.MethodName)
                            .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                            .AddParameterListParameters(
                                SyntaxFactory.Parameter(
                                        SyntaxFactory.Identifier(TemplateIDependencyInstaller.FirstParameterName))
                                            .WithType(
                                                SyntaxFactory.IdentifierName(TemplateIDependencyInstaller.FirstParameterType)))
                            .WithBody(SyntaxFactory.Block())]);

        var syntaxTree = CSharpSyntaxTree.Create(
            SyntaxFactory.CompilationUnit()
                .AddMembers(
                    SyntaxFactory.NamespaceDeclaration(
                        SyntaxFactory.ParseName(namespaceName))
                        .AddMembers(classDeclaration)));

        var semanticModel = CSharpCompilation.Create(assemblyName: "UnitTests", syntaxTrees: [syntaxTree])
            .GetSemanticModel(syntaxTree);
        var classDeclarationSyntax = (syntaxTree.GetRoot()
            .ChildNodes()
            .FirstOrDefault(x => x is NamespaceDeclarationSyntax) as NamespaceDeclarationSyntax)
            ?.Members
            .FirstOrDefault(x => x.IsEquivalentTo(classDeclaration));

        var expected = new DependencyInstallerInfo
        {
            FullName = $"{namespaceName}.{className}"
        };

        // Act
        var actual = _sut.RunQuery(semanticModel, classDeclarationSyntax);

        // Assert
        actual.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void RunQuery_ShouldReturnNull_WhenGivenSyntaxIsNotAssociatedWithANamedTypeSymbol()
    {
        // Arrange
        var methodDeclaration = SyntaxFactory.CompilationUnit()
            .AddMembers(
                SyntaxFactory.MethodDeclaration(
                        SyntaxFactory.ParseTypeName("void"),
                        TemplateIDependencyInstaller.MethodName)
                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                    .AddParameterListParameters(
                        SyntaxFactory.Parameter(
                                SyntaxFactory.Identifier(TemplateIDependencyInstaller.FirstParameterName))
                            .WithType(
                                SyntaxFactory.IdentifierName(TemplateIDependencyInstaller.FirstParameterType)))
                    .WithBody(SyntaxFactory.Block()));

        var syntaxTree = SyntaxFactory.SyntaxTree(methodDeclaration);
        var classDeclarationSyntax = syntaxTree.GetRoot().FindNode(methodDeclaration.FullSpan);
        var semanticModel = CSharpCompilation.Create(assemblyName: "UnitTests", syntaxTrees: [syntaxTree])
            .GetSemanticModel(syntaxTree);

        // Act
        var actual = _sut.RunQuery(semanticModel, classDeclarationSyntax);

        // Assert
        actual.Should().BeNull();
    }
}