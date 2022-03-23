using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_web.Models
{
    public class Category
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public int? CategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public List<Category> ChildCategories { get; set; }



    }
}
