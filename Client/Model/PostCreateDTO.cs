using System.ComponentModel.DataAnnotations;
namespace client.Model;

public class PostCreateDTO
{

    [Required]
    [StringLength(150, ErrorMessage = "Title must be over two characters.", MinimumLength = 2)]
    public string? Title { get; set; }
    public int AuthorId { get; set; }
    public DateTime Created { get; set;}
    public string Status { get; set;}

    [Required(ErrorMessage = "Please enter a description of your project.")]
    [StringLength(500)]
    public string Description { get; set; }
    public ICollection<KeywordDTO> Keywords { get; set; }


}