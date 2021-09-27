namespace ChatSystem.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AuthorEntity
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<MessageEntity> Messages { get; set; }
    }
}
