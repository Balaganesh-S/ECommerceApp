using ECommerceApp.Data;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ECommerceAppContext dbContext;
        public AddressRepository(ECommerceAppContext dbContext) {
            this.dbContext = dbContext;
        }
        public async Task<bool> CreateAddress(Address address)
        {
            await dbContext.Address.AddAsync(address);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAddress(int id)
        {
            Address address = await dbContext.Address.FindAsync(id);
            if (address == null) {
                return false;
            }
            dbContext.Address.Remove(address);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Address> GetAddressById(int id)
        {
            Address address = await dbContext.Address.FindAsync(id);
            return address;
        }

        public async Task<List<Address>> GetAddressByUserId(int userId)
        {
            return await dbContext.Address
            .Where(c => c.UserId == userId) 
            .ToListAsync();
        }

        public async Task<List<Address>> GetAllAddress()
        {
            return await dbContext.Address.ToListAsync();
        }

        public async Task<bool> UpdateAddress(int id,Address address)
        {
            Address address1 = await dbContext.Address
                .Include (a => a.User)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (address1 == null) {
                return false;
            }
            address1.Name = address.Name;
            address1.StreetName = address.StreetName;
            address1.City = address.City;
            address1.ZipCode = address.ZipCode;
            address1.Country = address.Country;
            address1.State = address.State;
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
