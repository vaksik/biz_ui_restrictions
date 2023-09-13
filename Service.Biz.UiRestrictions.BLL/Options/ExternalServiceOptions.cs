namespace Service.Biz.UiRestrictions.BLL.Options
{
    public class ExternalServiceOptions
    {
        public List<DomainService>? DomainServices { get; set; }
    }

    public class DomainService
    {
        public string Name { get; set; } = null!;

        public string Host { get; set; } = null!;
    }
}