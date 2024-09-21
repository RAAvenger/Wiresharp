using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Wiresharp.Microsoft.Extensions.DependencyInjection.SnapshotTests;

public class CommonFilesTests
{
    private readonly CSharpGeneratorDriver _driver;
    private readonly CodeGenerator _sut;

    public CommonFilesTests()
    {
        _sut = new CodeGenerator();
        _driver = CSharpGeneratorDriver.Create(_sut);
    }

    [Fact]
    public async Task ShouldAddIDependencyInstallerAndDependencyAttributeToSourceCode()
    {
        // Arrange
        var source = "";
        var syntaxTree = CSharpSyntaxTree.ParseText(source);
        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };
        var compilation = CSharpCompilation.Create(assemblyName: "SnapshotTests", syntaxTrees: [syntaxTree], references: references);

        // Act
        var actual = _driver.RunGenerators(compilation);

        // Assert
        await Verify(actual);
    }
}