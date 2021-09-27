namespace ChatSystem.Message.Importer
{
    using ChatSystem.Infrastructure.Common;
    using ChatSystem.Infrastructure.ConfigurationSettings;
    using ChatSystem.Infrastructure.Contracts;
    using ChatSystem.Infrastructure.Models;
    using ChatSystem.MessageHistoryAPI.Contracts;
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
        private readonly IMessageHistoryService messageService;
        private readonly ILogger<ApplicationService> logger;

        public ApplicationService(IntervalSettings settings,
            IMessenger messenger,
            IMessageHistoryService messageService,
            ILogger<ApplicationService> logger)
        {
            this.settings = settings;
            this.messenger = messenger;
            this.messageService = messageService;
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
                            // TDOD retry policy
                            var result = await messageService.InsertAsync(messages);
                            if (result)
                            {
                                messages.RemoveRange(messages.Count - BatchSize, BatchSize);
                            }
                            else
                            {
                                logger.LogError("Not able to save the following messages: ", message);
                            }
                        }
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
    }
}