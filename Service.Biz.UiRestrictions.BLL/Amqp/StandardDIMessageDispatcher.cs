using Microsoft.Extensions.DependencyInjection;
using Module.Amqp;

namespace Service.Biz.UiRestrictions.BLL.Amqp
{
    public class StandardDIMessageDispatcher : IMessageDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public StandardDIMessageDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Dispatch<TMessage, TConsumer>(TMessage message, MessageContext context)
            where TMessage : class
            where TConsumer : IConsumer<TMessage>
        {
            using (serviceProvider.CreateScope())
                await serviceProvider.GetRequiredService<TConsumer>().Consume(message, context);
        }
    }
}
