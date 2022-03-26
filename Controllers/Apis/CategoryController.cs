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
    public class CategoryController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> GetProducts()
        {

            // Pagination
            var length = int.Parse(Request.Form["length"]);

            var start = int.Parse(Request.Form["start"]);


            // Search
            var searchValue = Request.Form["search[value]"];

            var categories = _context.Categories.Where(p =>
                string.IsNullOrEmpty(searchValue) || p.Name.Contains(searchValue));


            // Sort
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
                categories = categories.OrderBy(string.Concat(sortColumn, " ", sortDirection));



            var data = await categories.Skip(start).Take(length).ToListAsync();

            var count = categories.Count();

            var jsonData = new { recordsFiltered = count, count, data };

            return Ok(jsonData);
        }
    }
  
}
