namespace Service.Biz.UiRestrictions.DAL.Entities;

public class OrganizationProduct
{
    public int ProductId { get; set; }
    
    public Guid OrganizationId { get; set; }
    
    public Guid? NetworkId { get; set; }

    public Product Product { get; set; } = null!;
}