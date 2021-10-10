﻿namespace ChatSystem.Data.Services
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
                var result = messages.OrderByDescending(m => m.TimeStamp)
                        .Take(takeCount)
                        .Select(m => new Message
                        {
                            AuthorId = m.AuthorId,
                            Author = m.Author.Name,
                            Text = m.Text,
                            TimeStamp = m.TimeStamp
                        });

                if (result.Any())
                {
                    return result;
                }
            }

            return null;
        }

        public IEnumerable<Message> GetPerTimePeriod(TimeFrame timeFrame, CancellationToken cancellationToken)
        {
            var messages = db.Messages;
            if (messages.Any())
            {
                var result = messages.Where(m => DateTime.Compare(m.TimeStamp, timeFrame.StartTime) > 0
                    && DateTime.Compare(m.TimeStamp,timeFrame.EndTime) < 0)
                        .Select(m => new Message
                        {
                            AuthorId = m.AuthorId,
                            Author = m.Author.Name,
                            Text = m.Text,
                            TimeStamp = m.TimeStamp
                        });

                if (result.Any())
                {
                    return result;
                }
            }

            return null;
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
