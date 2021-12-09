namespace Interfaces;
public interface IUserRepository
{
    // fetch data about an existing user (there is only one)
    Task<Option<UserDto>> ReadAsync(int UserId);

}