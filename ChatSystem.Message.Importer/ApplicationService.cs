namespace ChatSystem.Message.Importer
{
    using Infrastructure.Common;
    using Infrastructure.ConfigurationSettings;
    using Infrastructure.Contracts;
    using Infrastructure.Models;
    using MessageHistoryAPI.Contracts;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal class ApplicationService : BackgroundService
    {
        private const int BatchSize = 100;

        private readonly IntervalSettings settings;
        private readonly IMessenger messenger;
        private readonly IMessageRepository messageRepository;
        private readonly ILogger<ApplicationService> logger;

        public ApplicationService(IntervalSettings settings,
            IMessenger messenger,
            IMessageRepository messageRepository,
            ILogger<ApplicationService> logger)
        {
            this.settings = settings;
            this.messenger = messenger;
            this.messageRepository = messageRepository;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var messages = new List<Message>();
                try
                {
                    await foreach (var message in messenger.GetDirectAsync<Message>(Constants.Exchange, Constants.Queue, Constants.Key, stoppingToken))
                    {
                        messages.Add(message);
                        if (messages.Count > BatchSize)
                        {
                            await SaveMessageAsync(messages);
                            messages.RemoveRange(messages.Count - BatchSize, BatchSize);
                        }
                    }

                    if (messages.Count > 0)
                    {
                        await SaveMessageAsync(messages);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"{nameof(ApplicationService)}.{nameof(ExecuteAsync)}.{ex.Message}");
                }

                var timeSpan = new TimeSpan(
                    days: settings.Day,
                    hours: settings.Hour,
                    minutes: settings.Minute,
                    seconds: settings.Second);

                await Task.Delay(timeSpan, stoppingToken);
            }
        }

        private async Task SaveMessageAsync(List<Message> messages)
        {
            // TODO retry policy
            try
            {
                await messageRepository.InsertAsync(messages);
            }
            catch (Exception ex)
            {
                logger.LogError($"Not able to save messages: {ex.Message}", messages);
            }
        }
    }
}