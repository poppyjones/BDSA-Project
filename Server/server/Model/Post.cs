namespace Model;

public class Post 
    {
        public Post()
        {
            collaborators = new HashSet<User>();
        }

        public int Id { get; set;}

        [StringLength(50)]
        public string title { get; set; }

        public User creator { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; } 

        [DataType(DataType.DateTime)]
        public DateTime Ended { get; set; } 

        public virtual ICollection<User> collaborators { get; set; }

        public string status { get; set; }

        [StringLength(500)]        
        public string description { get; set; }

        public Collection<Keyword> keywords { get; set; }
    }
