namespace ChatSystem.App.Contracts
{
    using Models;
    using System.Collections.Generic;

    public interface IMessageService
    {
        IEnumerable<Message> GetLastMessages();
    }
}
