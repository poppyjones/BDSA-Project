using System.Collections.ObjectModel;
using user;
using keyword;
using System.ComponentModel.DataAnnotations;

namespace post 
{
    public class Post 
    {
        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Required]
        public User creator { get; set; }

        [DataType(DataType.DateTime)]
        public (DateTime, DateTime) TimeFrame { get; set; } 

        public Collection<User> collaborators { get; set; }

        [Required]
        public string status { get; set; }

        [StringLength(500)]        
        public string description { get; set; }

        public Collection<Keyword> keywords { get; set; }
    }
}