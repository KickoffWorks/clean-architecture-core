using AutoFixture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Sample.Core.Infrastructure.Tests.Integration.Integration;

public sealed class UserManagerIntegrationTests : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgres = new PostgreSqlBuilder()
        .WithImage("postgres:15-alpine")
        .WithDatabase("test")
        .WithUsername("test")
        .WithPassword("test")
        .Build();
    
    private UserManager<Sample.Core.Domain.Entities.ApplicationUser> _userManager;
    private readonly Fixture _fixture = new();
    
    public async Task InitializeAsync()
    {
        await _postgres.StartAsync();
        
        var connectionString = _postgres.GetConnectionString();
        var services = new ServiceCollection();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        services.AddIdentityCore<Sample.Core.Domain.Entities.ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        var serviceProvider = services.BuildServiceProvider();
        _userManager = serviceProvider.GetRequiredService<UserManager<Sample.Core.Domain.Entities.ApplicationUser>>();
        
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public Task DisposeAsync()
    {
        return _postgres.DisposeAsync().AsTask();
    }

}