namespace server.Model;

public record UserDTO(
                int Id,
                string Name,
                string Institution,
                string Degree,
                string Status,
                ICollection<Post> Posts
                );

