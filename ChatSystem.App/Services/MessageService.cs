namespace ChatSystem.App.Services
{
    using Contracts;
    using Models;
    using System.Collections.Generic;

    internal class MessageService : IMessageService
    {
        public IEnumerable<Message> GetLastMessages()
        {
            return new List<Message>();
        }
    }
}
