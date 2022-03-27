using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace E_commerce_web.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        public int? CategoryId { get; set; }
    }
}
