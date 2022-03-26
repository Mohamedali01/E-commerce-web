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
    public class SellersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Seller> _sellerManager;


        public SellersController(ApplicationDbContext context,UserManager<Seller> sellerManager)
        {
            _context = context;
            _sellerManager = sellerManager;
        }



        [HttpPost]
        public async Task<IActionResult> GetSellers()
        {

            // Pagination
            var length = int.Parse(Request.Form["length"]);

            var start = int.Parse(Request.Form["start"]);


            // Search
            var searchValue = Request.Form["search[value]"];

            var sellers = _context.Sellers.Where(a =>
                string.IsNullOrEmpty(searchValue) || a.FirstName.Contains(searchValue) || a.LastName.Contains(searchValue) || a.Email.Contains(searchValue) || a.PhoneNumber.Contains(searchValue));


            // Sort
            var sortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")];
            var sortDirection = Request.Form["order[0][dir]"];

            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortDirection)))
                sellers = sellers.OrderBy(string.Concat(sortColumn, " ", sortDirection));



            var data = await sellers.Skip(start).Take(length).ToListAsync();

            var count = sellers.Count();

            var jsonData = new { recordsFiltered = count, count, data };

            return Ok(jsonData);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteSeller(string id)
        {

            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();


            var seller = await _sellerManager.FindByIdAsync(id);

            if (seller == null)
                return NotFound();

            await _sellerManager.DeleteAsync(seller);

            return Ok();
        }
    }
}
