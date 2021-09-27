namespace ChatSystem.App.Contracts
{
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        IEnumerable<Message> GetLastMessages(CancellationToken cancellationToken);

        Task SendAsync<Message>(Message message);
    }
}
