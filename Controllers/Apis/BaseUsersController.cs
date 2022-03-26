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

namespace E_commerce_web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<BaseUser> _baseUserManager;




        public BaseUsersController(ApplicationDbContext context, UserManager<BaseUser> baseUserManager)
        {
            _context = context;
            _baseUserManager = baseUserManager;
        }



        [HttpPost]
        public async Task<IActionResult> GetBaseUsers()
        {

            // Pagination
            var length = int.Parse(Request.Form["length"]);

            var start = int.Parse(Request.Form["start"]);


            // Search
            var searchValue = Request.Form["search[value]"];

            var baseUsers = _context.BaseUsers.Where(a =>
                string.IsNullOrEmpty(searchValue) || a.FirstName.Contains(searchValue) || a.LastName.Contains(searchValue) || a.Email.Contains(searchValue) || a.PhoneNumber.Contains(searchValue));


            // Sort
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
                baseUsers = baseUsers.OrderBy(string.Concat(sortColumn, " ", sortDirection));



            var data = await baseUsers.Skip(start).Take(length).ToListAsync();

            var count = baseUsers.Count();

            var jsonData = new { recordsFiltered = count, count, data };

            return Ok(jsonData);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin(string id)
        {

            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();


            var admin = await _baseUserManager.FindByIdAsync(id);

            if (admin == null)
                return NotFound();

            await _baseUserManager.DeleteAsync(admin);

            return Ok();
        }
    }
}
