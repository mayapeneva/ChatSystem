namespace ChatSystem.App.Contracts
{
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IMessageService
    {
        IEnumerable<Message> GetLastMessages();

        Task SendAsync<Message>(Message message);
    }
}
