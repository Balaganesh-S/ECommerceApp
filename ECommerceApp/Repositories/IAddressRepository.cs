using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Repositories
{
    public interface IAddressRepository
    {
        public Task<bool> CreateAddress(Address address);

        public Task<List<Address>> GetAllAddress();

        public Task<Address> GetAddressById(int id);

        public Task<List<Address>> GetAddressByUserId(int UserId);

        public Task<bool> UpdateAddress(int id, Address address);

        public Task<bool> DeleteAddress(int id);
    }
}
