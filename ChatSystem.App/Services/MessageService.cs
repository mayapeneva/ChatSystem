namespace ChatSystem.App.Services
{
    using Contracts;
    using Infrastructure.Common;
    using Infrastructure.ConfigurationSettings;
    using Infrastructure.Contracts;
    using Infrastructure.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MessageService : IMessageService
    {
        private const int MessagesCount = 20;
        private readonly HttpClient httpClient;
        private readonly MessageAPISettings settings;
        private readonly IMessenger messenger;

        public MessageService(IHttpClientFactory httpClientFactory,
            MessageAPISettings settings,
            IMessenger messenger)
        {
            httpClient = httpClientFactory.CreateClient();
            this.settings = settings;
            this.messenger = messenger;
        }

        public async Task<Result<IEnumerable<Message>>> GetLastMessages(CancellationToken cancellationToken)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{settings.Endpoint}/{MessagesCount}");
                var response = await httpClient.SendAsync(request, cancellationToken);
                var responseString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Result<IEnumerable<Message>>(Constants.NoMessagesFound, InternalStatusCode.NotFound, Constants.NoMessagesFound);
                }

                var messages = JsonConvert.DeserializeObject<IEnumerable<Message>>(responseString);
                return new Result<IEnumerable<Message>>(messages);
            }
            catch (Exception ex)
            {
                return new Result<IEnumerable<Message>>(Constants.CouldNotLoadMessages, InternalStatusCode.InternalServerError, ex.Message);
            }
        }

        public async Task<Result<string>> SendAsync<Message>(Message message)
        {
            try
            {
                await messenger.PublishDirectAsync(Constants.Exchange, Constants.Queue, Constants.Key, message, CancellationToken.None);
                return new Result<string>(null);
            }
            catch (Exception ex)
            {
                return new Result<string>(Constants.CouldNotSaveMessages, InternalStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
