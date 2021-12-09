Namespace Model;
public class UserDTO
{
    public record UserDTO(
                int Id,
                string Name,
                string Institution,
                string Degree,
                string Status,
                [ForeignKey("PostId")] ICollection<Post> Posts
                );
}
