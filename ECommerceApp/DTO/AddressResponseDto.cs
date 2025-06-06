﻿namespace ECommerceApp.DTO
{
    public class AddressResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public int UserId { get; set; }
    }
}
