namespace server.Model;

public record PostCreateDTO(
                        string Title,
                        int AuthorId,
                        DateTime Created,
                        DateTime? Ended, //den her
                        string Status,
                        [StringLength(500)] string Description,
                        ICollection<KeywordDTO> Keywords,
                        ICollection<UserDTO> Users
                        );

public record PostDTO(
                    int Id,
                    string Title,
                    int AuthorId,
                    DateTime Created,
                    DateTime? Ended, //den her
                    string Status,
                    [StringLength(500)] string Description,
                    ICollection<KeywordDTO> Keywords,
                    ICollection<UserDTO> Users
                    );
