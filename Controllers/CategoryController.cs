using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_commerce_web.Data;
using E_commerce_web.Models;
using E_commerce_web.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace E_commerce_web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CategoryController(ApplicationDbContext context , IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Store(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Create");


            string uniqueFileName = ProcessUploadedFile(model);
            var row = new Category
            {
                Name = model.Name,
                Image = uniqueFileName
            };

            _context.Add(row);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private string ProcessUploadedFile(CategoryViewModel model)
        {
            string uniqueFileName = null;

            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                     model.Image.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

    }
}
