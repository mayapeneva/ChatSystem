namespace ChatSystem.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MessageEntity
    {
        [Required]
        [Key]
        public string Id { get; set; }
        
        [Required]
        public string AuthorId { get; set; }

        public virtual AuthorEntity Author { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime TimeStamp { get; set; }
    }
}
