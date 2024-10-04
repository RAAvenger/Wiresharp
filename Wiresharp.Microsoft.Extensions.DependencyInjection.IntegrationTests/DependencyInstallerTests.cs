using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Wiresharp;
using Xunit;

namespace IntegrationTests;

public class DependencyInstallerTests
{
    [Fact]
    public void DependencyInstaller_ShouldHaveInstallDependenciesMethod_WhenEver()
    {
        // Arrange
        var sut = new DependencyInstaller();

        // Act
        sut.InstallDependencies(new ServiceCollection());

        // Assert
        true.Should().BeTrue();
    }

    private class DependencyInstaller : IDependencyInstaller
    {
        public void InstallDependencies(IServiceCollection services)
        {
            // do nothing
        }
    }
}