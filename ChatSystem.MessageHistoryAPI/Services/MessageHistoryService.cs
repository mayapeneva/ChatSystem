namespace ChatSystem.MessageHistoryAPI.Services
{
    using Contracts;
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class MessageHistoryService : IMessageHistoryService
    {
        public Task<bool> InsertAsync(IEnumerable<Message> messages)
        {
            return Task.FromResult(true);
        }
    }
}
