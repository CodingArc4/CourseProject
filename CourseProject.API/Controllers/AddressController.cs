﻿using Courseproject.Common.Dtos.Address;
using Courseproject.Common.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAddress(AddressCreate addressCreate)
        {
            var id = await _addressService.CreateAddressAsync(addressCreate);
            return Ok(id);
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateAddress(AddressUpdate addressUpdate)
        {
            await _addressService.UpdateAddressAsync(addressUpdate);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteAddress(AddressDelete addressDelete)
        {
            await _addressService.DeleteAddressAsync(addressDelete);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetAddress(int id)
        {
            var address = await _addressService.GetAddressAsync(id);
            return Ok(address);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetAddresses()
        {
            var addresses = await _addressService.GetAddressesAsync();
            return Ok(addresses);
        }
    }
}
