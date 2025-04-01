using AutoMapper;
using ECommerceApp.Data;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceAppContext dbContext;
        private readonly IMapper mapper;
        public UserRepository(ECommerceAppContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(email));
            }

            return await dbContext.Users
                .Include(u => u.Address)
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
            User user = mapper.Map<User>(userDto);
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return mapper.Map<UserDto>(user);
        }

       
    }
}
