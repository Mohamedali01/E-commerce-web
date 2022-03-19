using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace E_commerce_web.Models
{

    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set;}   
    }

    [Table("Admins")]
    public class Admin : ApplicationUser
    {

    }

    [Table("Sellers")]
    public class Seller : ApplicationUser
    {

    }

    [Table("BaseUsers")]
    public class BaseUser : ApplicationUser
    {
        public string Address { get; set; }
    }

}
