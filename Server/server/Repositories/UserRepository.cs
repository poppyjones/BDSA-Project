namespace Repository;

public class UserRepository : IUserRepository
{

    private readonly IContext _context;

    public UserRepository(IContext context)
    {
        _context = context;
    }

    Task<Option<UserDto>> ReadAsync(int UserId)
    {
        throw NotImplementedException();
    }
    
}
