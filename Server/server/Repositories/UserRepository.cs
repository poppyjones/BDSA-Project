using server.Interfaces;
using server.Database;
using server.Model;

namespace server.Repository;

public class UserRepository : IUserRepository
{

    private readonly IContext _context;

    public UserRepository(IContext context)
    {
        _context = context;
    }

    public Task<UserDTO> ReadAsync(int UserId)
    {
        throw new NotImplementedException();
    }
    
}
