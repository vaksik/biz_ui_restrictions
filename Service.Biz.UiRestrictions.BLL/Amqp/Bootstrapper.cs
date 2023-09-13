using EasyNetQ.Topology;
using Microsoft.Extensions.DependencyInjection;
using Module.Amqp;
using Module.Logging.Core;
using Module.Tracing.Contract;
using Service.Tariffs.Amqp.Contracts;
using System.Text;

namespace Service.Biz.UiRestrictions.BLL.Amqp
{
    public static class Bootstrapper
    {
        private const string SuffixQueueName = ".biz_ui_restrictions";

        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IMessageBusFactory>(sp => new MessageBusFactory(
                sp.GetRequiredService<ILoggerFactory>(),
                sp.GetRequiredService<ITracer>(),
                new UTF8Encoding(false)));
            services.AddSingleton(sp => sp.GetRequiredService<IMessageBusFactory>()
                    .Create(connectionString, new StandardDIMessageDispatcher(sp)));

            services.AddSingleton<ProductUpdateConsumer>();
            return services;
        }

        public static Task<bool> InitAsync(this IMessageBus messageBus)
        {
            InitTopology(messageBus);
            messageBus.AdvancedBus.Connected += (sender, args) => messageBus.Start();
            if (messageBus.AdvancedBus.IsConnected)
                messageBus.Start();
            return Task.FromResult(true);
        }

        private static void InitTopology(IMessageBus messageBus)
        {
            InitTariffProductsUpdateTopology(messageBus);
        }

        private static void InitTariffProductsUpdateTopology(IMessageBus messageBus)
        {
            var exchange = messageBus.AdvancedBus.ExchangeDeclare(ProductUpdate.ExchangeName, ExchangeType.Topic);

            messageBus.Consume<ProductUpdate, ProductUpdateConsumer>(bus =>
            {
                var queue = bus.QueueDeclare(ProductUpdate.ExchangeName + SuffixQueueName, durable: false, autoDelete: true);
                bus.Bind(exchange, queue, ProductUpdate.RoutingKey);
                return queue;
            });
        }
    }
}
