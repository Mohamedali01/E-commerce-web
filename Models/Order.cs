using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_web.Models
{
    public class Order
    {

        public int Id { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public double TotalPrice { get; set; }

        public int Status { get; set; } // 0 => For waiting , 1 => in driving , 2 => done , 3 => under revision

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}
