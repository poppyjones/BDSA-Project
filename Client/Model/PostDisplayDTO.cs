using System.ComponentModel.DataAnnotations;
namespace client.Model;
public class PostDisplayDTO
{
    public int Id { get; set; }

    public string Title { get; set; }
    public int AuthorId { get; set; }
    public DateTime created { get; set; }
    public DateTime? Ended { get; set; }

    public string Status { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
    public ICollection<KeywordDTO> Keywords { get; set; }


}