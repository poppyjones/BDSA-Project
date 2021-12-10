using server.Interfaces;
using server.Database;
using server.Model;

namespace server.Repositories;

public class UserRepository : IUserRepository
{

    private readonly IContext _context;

    public UserRepository(IContext context)
    {
        _context = context;
    }

    public UserDTO ReadById(int UserId)
    {
        var user =  from u in _context.Users
                    where u.Id == UserId
                    select new UserDTO(
                        u.Id,
                        u.Name,
                        u.Email,
                        u.Institution,
                        u.Degree
                    );
        
        return user.FirstOrDefault();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
