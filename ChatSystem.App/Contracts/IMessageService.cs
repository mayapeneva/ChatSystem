namespace ChatSystem.App.Contracts
{
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task<Result<IEnumerable<Message>>> GetLastMessages(CancellationToken cancellationToken);

        Task<Result<string>> SendAsync<Message>(Message message);
    }
}
