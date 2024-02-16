using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Core.Infrastructure.Configurations.Users;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<Core.Domain.Entities.ApplicationUser>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ApplicationUser> builder)
    {
        builder.HasMany(u => u.Images) // User has many Images
            .WithOne(a => a.Owner) // Image belongs to one User
            .HasForeignKey(a => a.UserId) // Foreign key property in Image entity
            .OnDelete(DeleteBehavior.Cascade); 
        
        builder.ConfigureEntityProperties();
    }
}