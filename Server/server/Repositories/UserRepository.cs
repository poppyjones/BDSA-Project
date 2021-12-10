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

        // var characters = from c in _context.Characters
        //                  where c.Id == characterId
        //                  select new CharacterDetailsDto(
        //                      c.Id,
        //                      c.AlterEgo,
        //                      c.GivenName,
        //                      c.Surname,
        //                      c.City == null ? null : c.City.Name,
        //                      c.Gender,
        //                      c.FirstAppearance,
        //                      c.Occupation,
        //                      c.ImageUrl,
        //                      c.Powers.Select(c => c.Name).ToHashSet()
        //                  );

        // return await characters.FirstOrDefaultAsync();
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
