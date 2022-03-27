using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using E_commerce_web.Data;
using E_commerce_web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_commerce_web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Admin> _adminManager;



        public AdminsController(ApplicationDbContext context, UserManager<Admin> adminManager)
        {
            _context = context;
            _adminManager = adminManager;
        }



        [HttpPost]
        public async Task<IActionResult> GetAdmins()
        {

            // Pagination
            var length = int.Parse(Request.Form["length"]);

            var start = int.Parse(Request.Form["start"]);


            // Search
            var searchValue = Request.Form["search[value]"];

            var admins = _context.Admins.Where(a =>
                string.IsNullOrEmpty(searchValue) || a.FirstName.Contains(searchValue) || a.LastName.Contains(searchValue) || a.Email.Contains(searchValue) || a.PhoneNumber.Contains(searchValue));


            // Sort
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
                admins = admins.OrderBy(string.Concat(sortColumn, " ", sortDirection));



            var data = await admins.Skip(start).Take(length).ToListAsync();

            var count = admins.Count();

            var jsonData = new { recordsFiltered = count, count, data };

            return Ok(jsonData);
        }


        // DELETE  /api/admins?id=1
        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin(string id)
        {

            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();


            var admin =await _adminManager.FindByIdAsync(id);

            if (admin == null)
                return NotFound();

            await  _adminManager.DeleteAsync(admin);

            return Ok();
        }

    }
}
