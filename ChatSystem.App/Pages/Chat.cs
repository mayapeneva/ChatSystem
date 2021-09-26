﻿namespace ChatSystem.App.Pages
{
    using Contracts;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;

    public partial class Chat
    {
        [Inject]
        public IMessageService MessageService { get; set; }

        [Parameter]
        public Message Message { get; set; }

        public List<Message> Messages { get; set; }

        protected override void OnInitialized()
        {
            Message = new Message();
            Messages = new List<Message>(MessageService.GetLastMessages());
        }

        protected void Create()
        {
            if (Message.Text is null
                || (Message.Author is null))
            {
                return;
            }

            MessageService.SendAsync(Message);

            Messages.Add(Message);
            if (Messages.Count > 20)
            {
                Messages.RemoveAt(0);
            }

            var author = Message.Author;
            Message = new Message();
            Message.Author = author;
        }
    }
}
