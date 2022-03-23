using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce_web.Models;
using Microsoft.AspNetCore.Identity;

namespace E_commerce_web.Controllers
{
    public class AdminsController : Controller
    {
        private UserManager<Admin> _userManager;

        public AdminsController(UserManager<Admin> userManager)
        {
            _userManager = userManager;
        }
        public   IActionResult Index()
        {

            /*await _userManager.CreateAsync(new Admin
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                EmailConfirmed = true,
                FirstName = "Mohamed",
                LastName = "Ahmed"
            },"Mohamed9182@");*/

            return View();
        }
    }
}
