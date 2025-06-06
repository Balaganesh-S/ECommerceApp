﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
