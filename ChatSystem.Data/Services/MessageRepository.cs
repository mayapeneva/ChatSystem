namespace ChatSystem.Data.Services
{
    using Entities;
    using Infrastructure.Contracts;
    using Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    internal class MessageRepository : IMessageRepository
    {
        private readonly ChatSystemContext db;

        public MessageRepository(ChatSystemContext db)
        {
            this.db = db;
        }

        public IEnumerable<Message> Get(CancellationToken cancellationToken, int count)
        {
            var messages = db.Messages;
            if (messages.Any())
            {
                var dbMessagesCount = messages.Count();
                var takeCount = count > dbMessagesCount ? count : dbMessagesCount;
                return messages.OrderByDescending(m => m.TimeStamp)
                        .Take(takeCount)
                        .ToList()
                        .Select(m => new Message
                        {
                            AuthorId = m.AuthorId,
                            Author = m.Author.Name,
                            Text = m.Text,
                            TimeStamp = m.TimeStamp
                        });
            }

            return null;
        }

        public IEnumerable<Message> GetPerTimePeriod(TimeFrame timeFrame, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync(IEnumerable<Message> messages, CancellationToken cancellationToken)
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
