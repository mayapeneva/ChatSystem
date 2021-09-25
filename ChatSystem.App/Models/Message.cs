namespace ChatSystem.App.Models
{
    using System;

    public class Message
    {
        public Message()
        {
            TimeStamp = DateTime.UtcNow;
        }

        public string AuthorId { get; set; }

        public string Author { get; set; }

        public string Text { get; set; }

        public int TextLength => Text.Length;

        public DateTime TimeStamp { get; set; }
    }
}