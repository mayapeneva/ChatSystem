namespace ChatSystem.Infrastructure
{
    using ConfigurationSettings;
    using Contracts;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal class RabbitMQMessenger : IMessenger
    {
        private readonly RabbitMQSettings configuration;
        private readonly SemaphoreSlim semaphore;
        private readonly ConnectionFactory connectionFactory;
        private readonly IDictionary<string, object> queueDeclarationArguments;
        private IConnection connection;

        public RabbitMQMessenger(RabbitMQSettings settings)
        {
            configuration = settings;
            semaphore = new SemaphoreSlim(1, 1);
            connectionFactory = new ConnectionFactory
            {
                AutomaticRecoveryEnabled = true,
                UserName = configuration.Username,
                Password = configuration.Password,
                HostName = configuration.Host,
                Port = configuration.Port
            };
            queueDeclarationArguments = new Dictionary<string, object>()
            {
                { "x-queue-type", "quorum" }
            };
        }

        public async Task PublishAsync<T>(string exchange, string queue, string key, T message, CancellationToken cancellationToken)
        {
            if (message is null)
            {
                throw new Exception($"{nameof(IMessenger)}.{nameof(PublishAsync)}.{nameof(message)}");
            }

            if (!IsConnected)
            {
                await ConnectAsync(cancellationToken);
            }

            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclareNoWait(exchange, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                channel.QueueDeclareNoWait(queue, durable: true, exclusive: false, autoDelete: false, arguments: queueDeclarationArguments);
                channel.QueueBindNoWait(queue, exchange, key, null);
                channel.BasicPublish(exchange, key, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
            }
        }

        public Task GetAsync<T>(string exchange, string queue, string key, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task ConnectAsync(CancellationToken cancellationToken)
        {
            await semaphore.WaitAsync(cancellationToken);

            try
            {
                if (IsConnected)
                {
                    return;
                }

                connection = connectionFactory.CreateConnection();
            }
            finally
            {
                semaphore.Release();
            }
        }

        private bool IsConnected
        {
            get
            {
                return connection != null && connection.IsOpen;
            }
        }
    }
}
