namespace ChatSystem.Infrastructure
{
    using ConfigurationSettings;
    using Contracts;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using RabbitMQ.Client;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    internal class RabbitMQMessenger : IMessenger
    {
        private readonly RabbitMQSettings configuration;
        private readonly SemaphoreSlim semaphore;
        private readonly ConnectionFactory factory;
        private readonly IDictionary<string, object> queueDeclarationArguments;
        private IConnection connection;
        private readonly Logger<RabbitMQMessenger> logger;

        public RabbitMQMessenger(RabbitMQSettings settings,
            Logger<RabbitMQMessenger> logger)
        {
            configuration = settings;
            semaphore = new SemaphoreSlim(1, 1);
            factory = new ConnectionFactory
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

            this.logger = logger;
        }

        public async Task PublishDirectAsync<Message>(string exchange, string queue, string key, Message message, CancellationToken cancellationToken)
        {
            if (message is null)
            {
                throw new Exception($"{nameof(RabbitMQMessenger)}.{nameof(PublishDirectAsync)}.{nameof(message)}");
            }

            if (!IsConnected)
            {
                await ConnectAsync(cancellationToken);
            }

            using var channel = connection.CreateModel();
            channel.ExchangeDeclareNoWait(exchange, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclareNoWait(queue, durable: true, exclusive: false, autoDelete: false, arguments: queueDeclarationArguments);
            channel.QueueBindNoWait(queue, exchange, key, null);
            channel.BasicPublish(exchange, key, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
        }

        public async IAsyncEnumerable<Message> GetDirectAsync<Message>(string exchange, string queue, string key, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            if (!IsConnected)
            {
                await ConnectAsync(cancellationToken);
            }

            using var channel = connection.CreateModel();
            channel.ExchangeDeclareNoWait(exchange, ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
            channel.QueueDeclareNoWait(queue, durable: true, exclusive: false, autoDelete: false, arguments: queueDeclarationArguments);
            channel.QueueBindNoWait(queue, exchange, key, null);

            BasicGetResult result;
            Message message = default;
            while ((result = channel.BasicGet(queue, true)) != null)
            {
                var resultBody = Encoding.UTF8.GetString(result.Body.ToArray());
                try
                {
                    message = JsonConvert.DeserializeObject<Message>(resultBody);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"{nameof(RabbitMQMessenger)}.{nameof(GetDirectAsync)}.{resultBody}");
                }

                yield return message;
            }
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

                connection = factory.CreateConnection();
            }
            finally
            {
                semaphore.Release();
            }
        }

        private bool IsConnected => connection != null && connection.IsOpen;

        private Task SubscribeAsync(IModel channel, string queueName)
        {
            channel.BasicGet(queueName, false);
            return Task.CompletedTask;
        }
    }
}
