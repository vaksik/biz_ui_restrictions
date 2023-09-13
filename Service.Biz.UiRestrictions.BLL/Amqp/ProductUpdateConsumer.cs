using Module.Amqp;
using Service.Biz.UiRestrictions.BLL.Services;
using Service.Tariffs.Amqp.Contracts;

namespace Service.Biz.UiRestrictions.BLL.Amqp
{
    public class ProductUpdateConsumer : IConsumer<ProductUpdate>
    {
        private readonly IOrganizationService _organizationService;

        public ProductUpdateConsumer(IOrganizationService organizationManagerService)
        {
            _organizationService = organizationManagerService;
        }

        public Task Consume(ProductUpdate message, MessageContext context) =>
            _organizationService.UpdateOrganizationProductAsync(message.OrganizationId, message.ProductCode, message.NetworkId);
    }
}
