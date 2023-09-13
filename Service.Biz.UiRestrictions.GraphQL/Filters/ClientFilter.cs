using Service.Biz.UiRestrictions.GraphQL.Dto;
namespace Service.Biz.UiRestrictions.GraphQL.Filters;

public class ClientFilter
{
    public Guid Id { get; set; }
    
    public TypeOfClient ClientType { get; set; }
    

    public List<Guid> VisitedOrganizations { get; set; } = null!;
    
}