using System.Reflection;
using NetArchTest.Rules;

namespace Sample.Core.Tests.Architecture.Domain;

public class DomainLayerTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveAnyDependencies()
    {
        var assembly = Assembly.GetAssembly(typeof(Sample.Core.Domain.Settings.ApplicationSettings));

        var testResult = Types
            .InAssembly(assembly)
            .ShouldNot()
            .HaveDependencyOnAll("Sample.Core.Infrastructure")
            .GetResult();

        Assert.True(testResult.IsSuccessful);
    }
    
    [Fact]
    public void DomainLayer_AllClassesShouldBeSealed()
    {
        var assembly = Assembly.GetAssembly(typeof(Sample.Core.Domain.Settings.ApplicationSettings));

        var result = Types
            .InAssembly(assembly)
            .That()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}