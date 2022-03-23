using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_web.Models
{
    public class ProductImage
    {
        public int Id { get; set; }


        public int ProductId { get; set; }


        public byte [] Image { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
