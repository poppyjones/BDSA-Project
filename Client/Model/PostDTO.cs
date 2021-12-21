using System.ComponentModel.DataAnnotations;
namespace client.Model;
public class PostDTO
{
    public int Id { get; set; }

    [Required]
    [StringLength(20, ErrorMessage = "Topic must be over two characters.", MinimumLength = 2)]
    public string? Title { get; set; }
    public DateTime created { get; set; }
    public DateTime? Ended { get; set; }

    public string Status { get; set; }

    [Required(ErrorMessage = "Please enter a description of your project.")]
    [StringLength(500)]
    public string? Description { get; set; }
    public ICollection<KeywordDTO> Keywords { get; set; }

}