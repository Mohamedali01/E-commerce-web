using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

using E_commerce_web.Data;
using E_commerce_web.Models;
using Microsoft.EntityFrameworkCore;

namespace E_commerce_web.Controllers.Apis
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> GetProducts()
        {

            // Pagination
            var length =int.Parse(Request.Form["length"]);

            var start = int.Parse(Request.Form["start"]);


            // Search
            var searchValue = Request.Form["search[value]"];

            var products =  _context.Products.Where(p=> 
                string.IsNullOrEmpty(searchValue) || p.Name.Contains(searchValue) || p.Content.Contains(searchValue) || p.Rate.ToString().Contains(searchValue)|| p.Price.ToString().Contains(searchValue));


            // Sort
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
               products = products.OrderBy(string.Concat(sortColumn, " ", sortDirection));



            var data =await products.Skip(start).Take(length).ToListAsync();

            var count = products.Count();

            var jsonData = new { recordsFiltered = count, count, data };

            return Ok(jsonData);
        }
    }
}
