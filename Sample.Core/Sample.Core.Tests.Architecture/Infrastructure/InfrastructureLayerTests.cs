using System.Reflection;
using NetArchTest.Rules;

namespace Sample.Core.Tests.Architecture.Infrastructure;

public class InfrastructureLayerTests
{
    [Fact]
    public void InfrastructureLayer_ShouldDependOnDomainLayer()
    {
        var assembly = Assembly.GetAssembly(typeof(Sample.Core.Infrastructure.ApplicationDbContext));

        var result = Types
            .InAssembly(assembly)
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .HaveDependencyOn("Sample.Core.Domain")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}