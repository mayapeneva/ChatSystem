namespace ChatSystem.MessageHistoryAPI.Contracts
{
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageHistoryService
    {
        Task<bool> InsertAsync(IEnumerable<Message> messages);
    }
}
