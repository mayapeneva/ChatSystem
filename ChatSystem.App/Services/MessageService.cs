namespace ChatSystem.App.Services
{
    using ChatSystem.Infrastructure.Contracts;
    using Contracts;
    using Models;
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
            await messenger.SendAsync("chat-messages", "messages-queue", "message-key", message, CancellationToken.None);
        }
    }
}
