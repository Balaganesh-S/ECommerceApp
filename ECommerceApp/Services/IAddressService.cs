using ECommerceApp.Domain;
using ECommerceApp.DTO;

namespace ECommerceApp.Services
{
    public interface IAddressService
    {
        public Task<ResponseDto<AddressResponseDto>> CreateAddress(AddressDto addressDto);

        public Task<ResponseDto<List<AddressResponseDto>>> GetAllAddress();

        public Task<ResponseDto<AddressResponseDto>> GetAddressById(int id);

        public Task<ResponseDto<List<AddressResponseDto>>> GetAddressByUser();

        public Task<ResponseDto<AddressResponseDto>> UpdateAddress(int id, AddressDto addressDto);

        public Task<ResponseDto<string>> DeleteAddress(int id);
    }
}
