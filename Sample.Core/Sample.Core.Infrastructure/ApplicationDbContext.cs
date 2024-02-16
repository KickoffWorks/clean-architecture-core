using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sample.Core.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<Core.Domain.Entities.ApplicationUser>
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions options) : base (options)
    {
    }
    
    public ApplicationDbContext()
    {
        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .Build();
    }
    
    
    public DbSet<Core.Domain.Entities.ApplicationUser> Users { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = _configuration.GetConnectionString("Database")!;
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("sample");
        
        // Applies all entity configurations in this assembly
        
        ApplyConfigurations(builder);
        
        // Rename Users entity tables as they have a default value
        
        RenameIdentityTables(builder);
        
        // Follow Postgres table name convention
        
        RenameTablesFollowingConventions(builder);
    }

    private void RenameTablesFollowingConventions(ModelBuilder builder)
    {
        foreach(var entity in builder.Model.GetEntityTypes())
        {
            entity.SetTableName(ToSnakeCase(entity.GetTableName()!));
                
            foreach(var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.GetColumnName()!));
            }

            foreach(var key in entity.GetKeys())
            {
                key.SetName(ToSnakeCase(key.GetName()!));
            }

            foreach(var key in entity.GetForeignKeys())
            {
                key.SetConstraintName(ToSnakeCase(key.GetConstraintName()!));
            }

            foreach(var index in entity.GetIndexes())
            {
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
            }
        }
    }

    private void ApplyConfigurations(ModelBuilder builder)
    {
        var assembly = Assembly.GetAssembly(typeof(ApplicationDbContext))!;
        builder.ApplyConfigurationsFromAssembly(assembly);
    }

    private void RenameIdentityTables(ModelBuilder builder)
    {
        builder.Entity<Core.Domain.Entities.ApplicationUser>(b =>
        {
            b.ToTable("users");
        });

        builder.Entity<IdentityUserClaim<string>>(b =>
        {
            b.ToTable("user_claims");
        });

        builder.Entity<IdentityUserLogin<string>>(b =>
        {
            b.ToTable("user_logins");
        });

        builder.Entity<IdentityUserToken<string>>(b =>
        {
            b.ToTable("user_tokens");
        });

        builder.Entity<IdentityRole>(b =>
        {
            b.ToTable("roles");
        });

        builder.Entity<IdentityRoleClaim<string>>(b =>
        {
            b.ToTable("role_claims");
        });

        builder.Entity<IdentityUserRole<string>>(b =>
        {
            b.ToTable("user_roles");
        });
    }
    
    private string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input)) { return input; }

        var startUnderscores = Regex.Match(input, @"^_+");
        return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}
