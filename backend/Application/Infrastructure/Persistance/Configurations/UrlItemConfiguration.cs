using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Infrastructure.Persistance.Configurations;

public class UrlItemConfiguration : IEntityTypeConfiguration<UrlItem>
{
    public void Configure(EntityTypeBuilder<UrlItem> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasOne(t => t.Group)
            .WithMany(i => i.URLItems)
            .HasForeignKey(i => i.GroupId);

        builder.Property(i => i.Url)
            .IsRequired();

        builder.Property(i => i.CreationTime)
            .HasDefaultValueSql("getdate()");
    }
}