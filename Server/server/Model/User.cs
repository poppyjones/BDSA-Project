namespace Model;

    public class User 
    {
        public User()
        {
            collaborating_posts = new HashSet<Post>();
        }

        public int id { get; set; }

        public string name { get; set; }
        
        [DataType(DataType.EmailAddress)]
        public EmailAddressAttribute email { get; set; }

        public string institution { get; set; }

        public string degree { get; set; }

        public virtual ICollection<Post> collaborating_posts { get; set; }
    }
