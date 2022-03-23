using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_web.Models
{
    public class Product
    {


        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }


        public string Content { get; set; }

        [Required]
        public byte[] Image { get; set; }

        [Required]
        public string SellerId { get; set; }

        public Seller Seller { get; set; }


        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        public double Rate { get; set; }

        [Required]
        public double Price { get; set; }

        
        public int? OfferId { get; set; }

        public Offer Offer { get; set; }


        public List<ProductImage> Images { get; set; }
    }
}
