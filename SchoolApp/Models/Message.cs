using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public int UsersGroupsId { get; set; }

        [Required(ErrorMessage = "Continutul comentariului este obligatoriu")]
        public string? Content { get; set; }
    }

}
