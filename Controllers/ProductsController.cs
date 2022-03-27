using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce_web.Models;
using Microsoft.AspNetCore.Authorization;

namespace E_commerce_web.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
