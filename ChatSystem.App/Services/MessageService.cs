namespace ChatSystem.App.Services
{
    using ChatSystem.Infrastructure.Common;
    using Contracts;
    using Infrastructure.Contracts;
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MessageService : IMessageService
    {
        private readonly IMessenger messenger;

        public MessageService(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public IEnumerable<Message> GetLastMessages()
        {
            return new List<Message>();
        }

        public async Task SendAsync<Message>(Message message)
        {
            await messenger.PublishDirectAsync(Constants.Exchange, Constants.Queue, Constants.Key, message, CancellationToken.None);
        }
    }
}
