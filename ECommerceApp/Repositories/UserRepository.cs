using ECommerceApp.Data;
using ECommerceApp.Domain;

namespace ECommerceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceAppContext dbContext;
        public UserRepository(ECommerceAppContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
