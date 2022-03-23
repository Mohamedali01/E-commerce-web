using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce_web.Data;
using E_commerce_web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private ApplicationDbContext _context;


        public AdminsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public AdminsController(UserManager<Admin> userManager)
        {


        }


        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            var admins = await _context.Admins.ToListAsync();

            var count = admins.Count;

            var jsonData = new {recordsFiltered = count , count, admins};

            return Ok(jsonData);
        }
    }
}
