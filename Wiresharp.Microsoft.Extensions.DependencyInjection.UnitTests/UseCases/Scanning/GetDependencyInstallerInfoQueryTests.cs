using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Wiresharp.Microsoft.Extensions.DependencyInjection.CodeTemplates;
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
    public void RunQuery_ShouldReturnNull_WhenClassDeclarationSyntaxIsNotAssociatedWithANamedTypeSymbol()
    {
        // Arrange
        var classDeclaration = SyntaxFactory.ClassDeclaration("DummyInstaller")
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

        var semanticModel = GetSemanticModel(classDeclaration.ToString());

        // Act
        var actual = _sut.RunQuery(semanticModel, classDeclaration);

        // Assert
        actual.Should().BeNull();
    }

    //[Fact]
    //public void RunQuery_ShouldNotReturnNull_WhenClassDeclarationSyntaxIsNotAssociatedWithANamedTypeSymbol()
    //{
    //    // Arrange
    //    var tree = SyntaxTree(
    //    //CODE FROM ROSLYN QUOTER:
    //    CompilationUnit()
    //    .WithMembers(
    //        SingletonList<MemberDeclarationSyntax>(
    //            ClassDeclaration("DummyInstaller")
    //            .WithAttributeLists(
    //                SingletonList<AttributeListSyntax>(
    //                    AttributeList(
    //                        SingletonSeparatedList<AttributeSyntax>(
    //                            Attribute(
    //                                IdentifierName("InjectDependency"))))))
    //            .WithModifiers(
    //                TokenList(
    //                    Token(SyntaxKind.InternalKeyword)))
    //            .WithBaseList(
    //                BaseList(
    //                    SingletonSeparatedList<BaseTypeSyntax>(
    //                        SimpleBaseType(
    //                            IdentifierName("IDependencyInstaller")))))
    //            .WithMembers(
    //                SingletonList<MemberDeclarationSyntax>(
    //                    MethodDeclaration(
    //                        PredefinedType(
    //                            Token(SyntaxKind.VoidKeyword)),
    //                        Identifier("InstallDependencies"))
    //                    .WithModifiers(
    //                        TokenList(
    //                            Token(SyntaxKind.PublicKeyword)))
    //                    .WithParameterList(
    //                        ParameterList(
    //                            SingletonSeparatedList<ParameterSyntax>(
    //                                Parameter(
    //                                    Identifier("services"))
    //                                .WithType(
    //                                    IdentifierName("IServiceCollection")))))
    //                    .WithBody(
    //                        Block())))))
    //    .NormalizeWhitespace()
    //    //END
    //    );
    //    var root = tree.GetCompilationUnitRoot();
    //    var compilation = CSharpCompilation.Create("HelloWorld")
    //        .AddReferences(MetadataReference.CreateFromFile(typeof(string).Assembly.Location))
    //        .AddSyntaxTrees(tree);
    //    var semanticModel = compilation.GetSemanticModel(tree);
    //    var classDeclaration = SyntaxFactory.ClassDeclaration("DummyInstaller")
    //    .WithAttributeLists(
    //        SingletonList<AttributeListSyntax>(
    //            AttributeList(
    //                SingletonSeparatedList<AttributeSyntax>(
    //                    Attribute(
    //                        IdentifierName("InjectDependency"))))))
    //    .WithModifiers(
    //        TokenList(
    //            Token(SyntaxKind.InternalKeyword)))
    //    .WithBaseList(
    //        BaseList(
    //            SingletonSeparatedList<BaseTypeSyntax>(
    //                SimpleBaseType(
    //                    IdentifierName("IDependencyInstaller")))))
    //    .WithMembers(
    //        SingletonList<MemberDeclarationSyntax>(
    //            MethodDeclaration(
    //                PredefinedType(
    //                    Token(SyntaxKind.VoidKeyword)),
    //                Identifier("InstallDependencies"))
    //            .WithModifiers(
    //                TokenList(
    //                    Token(SyntaxKind.PublicKeyword)))
    //            .WithParameterList(
    //                ParameterList(
    //                    SingletonSeparatedList<ParameterSyntax>(
    //                        Parameter(
    //                            Identifier("services"))
    //                        .WithType(
    //                            IdentifierName("IServiceCollection")))))
    //            .WithBody(
    //                Block())));
    //    // Act
    //    _sut.RunQuery(semanticModel,

    //    // Assert
    //}

    private static SemanticModel GetSemanticModel(string code)
    {
        var tree = CSharpSyntaxTree.ParseText(code);
        var compilation = CSharpCompilation.Create("HelloWorld")
             .AddReferences(MetadataReference.CreateFromFile(typeof(string).Assembly.Location))
             .AddSyntaxTrees(tree);
        return compilation.GetSemanticModel(tree);
    }
}