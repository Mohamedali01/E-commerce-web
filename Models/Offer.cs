using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_web.Models
{
    public class Offer
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        public int Type { get; set; } // 0 => number , 1 => Percentage

        [Required]
        public double Value { get; set; }

        public DateTime ExpireDate { get; set; }



    }
}
