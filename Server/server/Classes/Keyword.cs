using System.ComponentModel.DataAnnotations;

namespace keyword {

    public class Keyword
    {
        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        public int id { get; set; }
    }
}