namespace ChatSystem.App.Services
{
    using Contracts;
    using Infrastructure.Common;
    using Infrastructure.Contracts;
    using Infrastructure.Models;
    using MessageHistoryAPI.Contracts;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MessageService : IMessageService
    {
        private readonly IMessageRepository messageRepository;
        private readonly IMessenger messenger;

        public MessageService(IMessageRepository messageRepository,
            IMessenger messenger)
        {
            this.messageRepository = messageRepository;
            this.messenger = messenger;
        }

        public IEnumerable<Message> GetLastMessages()
        {
            return messageRepository.Get();
        }

        public async Task SendAsync<Message>(Message message)
        {
            await messenger.PublishDirectAsync(Constants.Exchange, Constants.Queue, Constants.Key, message, CancellationToken.None);
        }
    }
}
