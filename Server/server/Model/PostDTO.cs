Namespace Model;

public class PostDTO
{
    public record PostCreateDTO(
                            string Title,
                            int AuthorId,
                            DateTime Created,
                            Datetime Created,
                            string Status,
                            [StringLength(500)] string Description,
                            ICollection<Keyword> Keywords,
                            [ForeignKey("UserId")] ICollection<User> Users
                            );

    public record PostDTO(
                        int Id,
                        string Title,
                        int AuthorId,
                        DateTime Created,
                        Datetime Created,
                        string Status,
                        [StringLength(500)] string Description,
                        ICollection<Keyword> Keywords,
                        [ForeignKey("UserId")] ICollection<User> Users
                        );

}