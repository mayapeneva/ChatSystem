namespace ChatSystem.App.Pages
{
    using Contracts;
    using Infrastructure.Models;
    using Microsoft.AspNetCore.Components;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class Chat
    {
        [Inject]
        public IMessageService MessageService { get; set; }

        [Parameter]
        public Message Message { get; set; }

        public List<Message> Messages { get; set; }

        protected override async void OnInitialized()
        {
            Message = new Message();

            var messages = await MessageService.GetLastMessages(cancellationToken: CancellationToken.None);
            Messages = messages.IsSuccess
                ? new List<Message>(messages.Data)
                : new List<Message>();
        }

        protected async Task Create()
        {
            if (Message.Text is null
                || (Message.Author is null))
            {
                return;
            }

            //TODO use connection ID or add User Identity
            Message.AuthorId = Guid.NewGuid().ToString();
            var result = await MessageService.SendAsync(Message);

            if (result.IsSuccess)
            {
                Messages.Add(Message);
            }
            else
            {
                // TODO: return validation error
            }

            if (Messages.Count > 20)
            {
                Messages.RemoveAt(0);
            }

            var author = Message.Author;
            Message = new Message
            {
                Author = author
            };
        }
    }
}
