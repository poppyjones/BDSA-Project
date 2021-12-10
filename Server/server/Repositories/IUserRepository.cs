using server.Database;
using server.Model;

namespace server.Interfaces;
public interface IUserRepository
{
    // fetch data about an existing user (there is only one)
    UserDTO ReadById(int UserId);

}