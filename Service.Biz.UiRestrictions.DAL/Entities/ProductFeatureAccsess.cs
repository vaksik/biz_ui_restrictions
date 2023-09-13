namespace Service.Biz.UiRestrictions.DAL.Entities;

public class ProductFeatureRestriction
{
    public int ProductId { get; set; }
    
    public int FeatureId { get; set; }
    
    public AccessType AccessType { get; set; }
    
    public AccessRestrictionType AccessRestrictionType { get; set; }
    
    public string? Detail { get; set; }
    
    public Feature Feature { get; set; } = null!;

    public Product Product { get; set; } = null!;
}