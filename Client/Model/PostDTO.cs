using System.ComponentModel.DataAnnotations;

public class PostDTO
{
    public int Id {get; set;}

    [Required]
    [StringLength(20, ErrorMessage = "Topic must be over two characters.", MinimumLength = 2)]
    public string? Topic {get; set;}

    [Required(ErrorMessage = "Please enter a description of your project.")]
    [StringLength(500)]    
    public string? Description {get; set;}
    public string? Keywords {get; set;}

}