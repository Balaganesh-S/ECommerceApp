using ECommerceApp.Domain;

namespace ECommerceApp.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
