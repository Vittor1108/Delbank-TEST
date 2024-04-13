using Delbank.Commons.Interfaces;
using Delbank.Commons;
using Delbank.Messaging.Interfaces;
using Delbank.Messaging;
using RabbitMQ;
using Delbank.Messaging.Publishers;
using Delbank.Messaging.Consumers;

namespace Delbank.Application.Injectors
{
    public static class _MessagingInjector
    {
        public static IServiceCollection MessagingInjector(this IServiceCollection services)
        {
            services.AddSingleton<IMessagingConnection>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var hostName = configuration["localhost"];
                return new MessagingConnection("localhost");
            });

            services.AddHostedService<ConsumerInsertData>();
            services.AddHostedService<ConsumerDeletedData>();
            services.AddHostedService<ConsumerUpdate>();

            services.AddScoped<IPublisher, Publisher>();            
            return services;
        }
    }
}
