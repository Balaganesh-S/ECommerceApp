﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.DTO
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        
    }
}
