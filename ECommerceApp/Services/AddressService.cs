using AutoMapper;
using ECommerceApp.Domain;
using ECommerceApp.DTO;
using ECommerceApp.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Security.Claims;

namespace ECommerceApp.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUserRepository userRepository;
        public AddressService(IAddressRepository addressRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.userRepository = userRepository;
        }

        public async Task<ResponseDto<AddressResponseDto>> CreateAddress(AddressDto addressDto)
        {
            var userEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }

            User user = await userRepository.GetUserByEmailAsync(userEmail);

            if (user == null)
            {
                return new ResponseDto<AddressResponseDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "User not found",
                    Data = null
                };
            }

            
            Address address = mapper.Map<Address>(addressDto);
            address.UserId = user.Id; 

            bool isCreated = await addressRepository.CreateAddress(address);

            if (!isCreated)
            {
                return new ResponseDto<AddressResponseDto>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Can't create address",
                    Data = null
                };
            }

            return new ResponseDto<AddressResponseDto>
            {
                StatusCode = HttpStatusCode.Created,
                Message = "New Address Created Successfully",
                Data = mapper.Map<AddressResponseDto>(address)
            };
        }


        public async Task<ResponseDto<string>> DeleteAddress(int id)
        {
            bool isDeleted = await addressRepository.DeleteAddress(id);
            if (!isDeleted) {
                return new ResponseDto<string>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Can't able to delete",
                    Data = null
                };
            }
            return new ResponseDto<string>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Address Deleted Successfully",
                Data = null
            };
        }

        public async Task<ResponseDto<AddressResponseDto>> GetAddressById(int id)
        {
            Address address = await addressRepository.GetAddressById(id);
            if (address == null) {
                return new ResponseDto<AddressResponseDto>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Can't able to fetch",
                    Data = null
                };
            }
            return new ResponseDto<AddressResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Successfully fetched",
                Data = mapper.Map<AddressResponseDto>(address)
            };
        }

        public async Task<ResponseDto<List<AddressResponseDto>>> GetAddressByUser()
        {
            var userEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                throw new UnauthorizedAccessException("User is not logged in.");
            }
            User user = await userRepository.GetUserByEmailAsync(userEmail);
            List<Address> addresses = await addressRepository.GetAddressByUserId(user.Id);
            List<AddressResponseDto> addressResponseDtos = mapper.Map<List<AddressResponseDto>>(addresses);
            return new ResponseDto<List<AddressResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "User Addresses fetched successfully",
                Data = addressResponseDtos
            };

        }

        public async Task<ResponseDto<List<AddressResponseDto>>> GetAllAddress()
        {
            List<Address> addresses = await addressRepository.GetAllAddress();
            return new ResponseDto<List<AddressResponseDto>>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "All the address are fetched successfully",
                Data = mapper.Map<List<AddressResponseDto>>(addresses)
            };
        }

        public async Task<ResponseDto<AddressResponseDto>> UpdateAddress(int id, AddressDto addressDto)
        {
            Address address = mapper.Map<Address>(addressDto);
            bool isUpdated = await addressRepository.UpdateAddress(id, address);
            if (!isUpdated) { 
                return new ResponseDto<AddressResponseDto> { 
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Address not updated",
                    Data = null
                };
            }
            return new ResponseDto<AddressResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = "Address updated successfully",
                Data = mapper.Map<AddressResponseDto>(address)
            };
        }
    }
}
