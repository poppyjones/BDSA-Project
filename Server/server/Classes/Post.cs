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
        public string title;

        public User creator;

        public (DateTime, DateTime) TimeFrame; 

        public Collection<User> collaborators;

        public string status;

        [StringLength(500)]        
        public string description;

        public Collection<Keyword> keywords;
    }
}