﻿namespace ChatSystem.Data
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public class ChatSystemContext : DbContext
    {
        public ChatSystemContext(DbContextOptions<ChatSystemContext> options)
            : base(options)
        {
        }

        public DbSet<MessageEntity> Messages { get; set; }

        public DbSet<AuthorEntity> Authors { get; set; }
    }
}