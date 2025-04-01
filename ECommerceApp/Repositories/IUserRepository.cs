using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Repositories
{
    public interface IUserRepository
    {
        Task<UserDto> CreateUserAsync(UserDto userDto);
        Task<User> GetUserByEmailAsync(string email);


    }
}
