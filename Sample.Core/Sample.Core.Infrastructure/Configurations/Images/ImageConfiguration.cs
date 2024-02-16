using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Core.Infrastructure.Configurations.Images;

public class ImageConfiguration : IEntityTypeConfiguration<Core.Domain.Entities.Image>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Image> builder)
    {
        builder.HasKey(d => d.Id);
        
        builder.HasOne(a => a.Owner)
            .WithMany(u => u.Images)
            .HasForeignKey(a => a.UserId);
        
        builder.ConfigureEntityProperties();
    }
}