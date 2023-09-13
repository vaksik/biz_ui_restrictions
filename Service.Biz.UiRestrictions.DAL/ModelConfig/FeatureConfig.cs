using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Biz.UiRestrictions.DAL.Entities;

namespace Service.Biz.UiRestrictions.DAL.ModelConfig;

public class FeatureConfig: IEntityTypeConfiguration<Feature>
{
    public void Configure(EntityTypeBuilder<Feature> builder)
    {
        builder.ToTable("feature");
        
        builder.Property(e => e.Id).HasColumnName("id");
        builder.HasKey(f => f.Id);
        builder.Property(e => e.Name).HasColumnName("name").HasMaxLength(256);
        builder.HasIndex(f => f.Name).IsUnique();
    }
}