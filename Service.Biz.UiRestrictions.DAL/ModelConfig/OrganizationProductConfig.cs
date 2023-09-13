using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Service.Biz.UiRestrictions.DAL.Entities;

namespace Service.Biz.UiRestrictions.DAL.ModelConfig;

public class OrganizationProductConfig: IEntityTypeConfiguration<OrganizationProduct>
{
    public void Configure(EntityTypeBuilder<OrganizationProduct> builder)
    {
        builder.ToTable("organization_product");
        builder.HasKey(f => new {f.OrganizationId, f.ProductId});
        builder.Property(e => e.OrganizationId).HasColumnName("organization_id");
        builder.Property(e => e.ProductId).HasColumnName("product_id");
        builder.Property(e => e.NetworkId).HasColumnName("network_id");
        builder.HasIndex(x => x.OrganizationId);
        builder.HasIndex(x => x.NetworkId);
    }
}