using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce_web
{
    public class testController : Controller
    {
        public async Task<IActionResult> Index()
        {

            // Get the image from the form
            var image = Request.Form.Files.FirstOrDefault();


            // Open connection
            var dataStream = new MemoryStream();

           await image.CopyToAsync(dataStream);

           // image as byte[]
           var imageAsBytes = dataStream.ToArray();

           return Ok();
        }
    }
}
