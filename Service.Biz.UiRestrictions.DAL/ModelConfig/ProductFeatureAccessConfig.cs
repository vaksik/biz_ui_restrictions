using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Biz.UiRestrictions.DAL.Entities;

namespace Service.Biz.UiRestrictions.DAL.ModelConfig;

public class ProductFeatureAccessConfig: IEntityTypeConfiguration<ProductFeatureRestriction>
{
    public void Configure(EntityTypeBuilder<ProductFeatureRestriction> builder)
    {
        builder.ToTable("product_feature_access");
        builder.HasKey(f => new {f.ProductId, f.FeatureId});
        builder.Property(e => e.ProductId).HasColumnName("product_id");
        builder.Property(e => e.FeatureId).HasColumnName("feature_id");
        builder.Property(e => e.AccessType).HasColumnName("access_type");
        builder.Property(e => e.AccessRestrictionType).HasColumnName("access_restriction_type");
        builder.Property(e => e.Detail).HasColumnName("detail").HasMaxLength(512);
        builder.HasIndex(f => f.FeatureId);
    }
}