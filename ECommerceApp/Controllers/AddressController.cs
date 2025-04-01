using ECommerceApp.DTO;
using ECommerceApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ECommerceApp.Controllers
{
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService addressService;
        public AddressController(IAddressService addressService) {
            this.addressService = addressService;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateAddress([FromBody] AddressDto addressDto)
        {
            var response = await addressService.CreateAddress(addressDto);

            if (response.StatusCode == HttpStatusCode.Created)
                return CreatedAtAction(nameof(CreateAddress), new { id = response.Data.ZipCode }, response.Data);

            return BadRequest(response);
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAllAddresses()
        {
            var response = await addressService.GetAllAddress();

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet]
        [Route("api/[controller]/{addressId}")]
        public async Task<IActionResult> GetAddressById([FromRoute] int addressId)
        {
            var response = await addressService.GetAddressById(addressId);
            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet]
        [Route("api/User/addresses")]
        public async Task<IActionResult> GetAddressByUser()
        {
            var response = await addressService.GetAddressByUser();
            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpPut]
        [Route("api/[controller]/{addressId}")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int addressId, [FromBody] AddressDto addressDto)
        {
            var response = await addressService.UpdateAddress(addressId, addressDto);
            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete]
        [Route("api/[controller]/{addressId}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int addressId)
        {
            var response = await addressService.DeleteAddress(addressId);
            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);
            return BadRequest(response);

        }
    }
}
