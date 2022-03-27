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
            var files = Request.Form.Files;

            var image = files.FirstOrDefault();
            //Ensure that we have a form 
            if (!files.Any())
            {
                ModelState.AddModelError("Poster", "Please Add movie image");
               
                // Should return view instead
                return Ok();
            }


            var extensions = new List<string> { ".jpg", ".png" };


            if (!extensions.Contains(Path.GetExtension(image.FileName).ToLower()))
            {
                ModelState.AddModelError("Poster", "Image extension is not acceptable");

                // Should return view instead
                return Ok();
            }


            if (image.Length > 1048576)
            {
                ModelState.AddModelError("Poster", "Image size can't be bigger than 1 MB");
                // Should return view instead
                return Ok();
            }




            // Open connection
            var dataStream = new MemoryStream();

           await image.CopyToAsync(dataStream);


           // image as byte[]
           var imageAsBytes = dataStream.ToArray();

           return Ok();
            
        }
    }
}
