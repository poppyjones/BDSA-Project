namespace server.Model
{

    public class Post
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public int AuthorId { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Ended { get; set; }

        public string Status { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Keyword> Keywords { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
