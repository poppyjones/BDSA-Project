namespace Model;

    public class User 
    {
        [Required]
        public string name { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public EmailAddressAttribute email { get; set; }

        [Required]
        public string institution { get; set; }

        [Required]
        public string degree { get; set; }

        public Collection<Post> collaborating_posts { get; set; }
    }
