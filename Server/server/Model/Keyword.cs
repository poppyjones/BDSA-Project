namespace Model;

public class Keyword
    {
        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        public int id { get; set; }
    }
