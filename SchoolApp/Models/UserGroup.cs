using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public class UserGroup
    {
        [Key] 
        public int UserGroupId { get; set; }

        public string? UserId { get; set; }

        public int? GroupId { get; set; } 

        public bool IsModerator { get; set; }
    }
}
