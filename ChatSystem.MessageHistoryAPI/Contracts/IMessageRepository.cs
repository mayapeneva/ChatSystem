namespace ChatSystem.MessageHistoryAPI.Contracts
{
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageRepository
    {
        IEnumerable<Message> Get();

        Task InsertAsync(IEnumerable<Message> messages);
    }
}
