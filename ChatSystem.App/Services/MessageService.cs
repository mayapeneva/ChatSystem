namespace ChatSystem.App.Services
{
    using Contracts;
    using Infrastructure.Common;
    using Infrastructure.Contracts;
    using Infrastructure.Models;
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

        public IEnumerable<Message> GetLastMessages(CancellationToken cancellationToken)
        {
            //TODO: call the API through an httpClient
            return messageRepository.Get(cancellationToken, 20);
        }

        public async Task SendAsync<Message>(Message message)
        {
            await messenger.PublishDirectAsync(Constants.Exchange, Constants.Queue, Constants.Key, message, CancellationToken.None);
        }
    }
}
