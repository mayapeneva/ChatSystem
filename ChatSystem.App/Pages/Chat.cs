namespace ChatSystem.App.Pages
{
    using Contracts;
    using Microsoft.AspNetCore.Components;
    using Models;
    using System.Collections.Generic;

    public partial class Chat
    {
        [Inject]
        public IMessageService MessageService { get; set; }

        [Parameter]
        public Message Message { get; set; }

        public string Author { get; set; }

        public List<Message> Messages { get; set; }

        protected override void OnInitialized()
        {
            Message = new Message();
            Messages = new List<Message>(MessageService.GetLastMessages());
        }

        protected void Create()
        {
            if (Message.Author != null)
            {
                Author = Message.Author;
            }

            Messages.Add(Message);
            if (Messages.Count > 20)
            {
                Messages.RemoveAt(0);
            }

            Message = new Message();
            Message.Author = Author;
        }
    }
}
