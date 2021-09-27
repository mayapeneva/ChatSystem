namespace ChatSystem.MessageHistoryAPI.Services
{
    using Data;
    using Data.Entities;
    using Contracts;
    using Infrastructure.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    internal class MessageRepository : IMessageRepository
    {
        private readonly ChatSystemContext db;

        public MessageRepository(ChatSystemContext db)
        {
            this.db = db;
        }

        public IEnumerable<Message> Get()
        {
            var messages = db.Messages?.OrderByDescending(m => m.TimeStamp)?.Take(20);
            return messages?.Select(m => new Message
            {
                AuthorId = m.AuthorId,
                Author = m.Author.Name,
                Text = m.Text,
                TimeStamp = m.TimeStamp
            });
        }

        public async Task InsertAsync(IEnumerable<Message> messages)
        {
            var dbMessages = messages.Select(m => new MessageEntity
            {
                Id = Guid.NewGuid().ToString(),
                Author = new AuthorEntity
                {
                    Id = m.AuthorId,
                    Name = m.Author
                },
                Text = m.Text,
                TimeStamp = m.TimeStamp
            });

            await db.Messages.AddRangeAsync(dbMessages);
            await db.SaveChangesAsync();
        }
    }
}
