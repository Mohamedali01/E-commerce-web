using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_commerce_web.Models;
using E_commerce_web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace E_commerce_web.Controllers
{
    public class AdminsController : Controller
    {
        private readonly UserManager<Admin> _adminManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminsController(UserManager<Admin> adminManager, UserManager<ApplicationUser> userManager)
        {
            _adminManager = adminManager;
            _userManager = userManager;
        }
        public  IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AdminsForm(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
                return View("AddAdminsForm",new AddUserFormViewModel());

            var admin =await _adminManager.FindByIdAsync(id);

            if (admin == null)
                return NotFound();
            var adminViewModel = new EditUserFormViewModel
            {
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                PhoneNumber = admin.PhoneNumber,
                UserName = admin.UserName,
            };
            return View("EditAdminsForm",adminViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAdmin(AddUserFormViewModel userFormViewModel)
        {
            if (!ModelState.IsValid)
                return View("AddAdminsForm", userFormViewModel);

            //Valid Email
            var email =await  _userManager.FindByEmailAsync(userFormViewModel.Email);

            if (email != null)
            {
                ModelState.AddModelError("Email","This email is already exists!");
                return View("AddAdminsForm", userFormViewModel);
            }

            //Valid Username
            var userName = await _userManager.FindByNameAsync(userFormViewModel.UserName);

            if (userName != null)
            {
                ModelState.AddModelError("UserName", "This username is already exists!");
                return View("AddAdminsForm", userFormViewModel);
            }

            var admin = new Admin
            {
                Email = userFormViewModel.Email,
                FirstName = userFormViewModel.FirstName,
                LastName = userFormViewModel.LastName,
                PhoneNumber = userFormViewModel.PhoneNumber,
                UserName = userFormViewModel.UserName
            };
           var result = await _adminManager.CreateAsync(admin, userFormViewModel.Password);

           if (!result.Succeeded)
           {
               foreach (var error in result.Errors)
               {
                   ModelState.AddModelError("PhoneNumber", error.Description);
                   return View("AddAdminsForm", userFormViewModel);
               }
           }

           await _adminManager.AddToRoleAsync(admin, RoleName.Admin);

            return RedirectToAction(nameof(Index));
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(EditUserFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("EditAdminsForm", viewModel);

            if (string.IsNullOrWhiteSpace(viewModel.Id))
                return BadRequest();

            var admin = await _adminManager.FindByIdAsync(viewModel.Id);

            if (admin == null)
                return NotFound();

            var userByEmail = await _userManager.FindByEmailAsync(viewModel.Email);

            if (userByEmail != null && userByEmail.Id != viewModel.Id)
            {
                ModelState.AddModelError("Email","This email is already exist!");
                return View("EditAdminsForm", viewModel);
            }
            var userByUsername = await _userManager.FindByNameAsync(viewModel.UserName);
            if (userByUsername != null && userByUsername.Id != viewModel.Id)
            {
                ModelState.AddModelError("UserName","This username is already exist!");
                return View("EditAdminsForm", viewModel);
            }


            //Map data
            admin.Email = viewModel.Email;
            admin.UserName = viewModel.UserName;
            admin.FirstName = viewModel.FirstName;
            admin.LastName = viewModel.LastName;
            admin.PhoneNumber = viewModel.PhoneNumber;

            await _adminManager.UpdateAsync(admin);

            return RedirectToAction(nameof(Index));

        }
    }
}








































/*
 *
 *             await _userManager.CreateAsync(new Admin{FirstName = "Mohamed",LastName = "Ali",Email = "mohamed@gmail.com",PhoneNumber = "01120352774"},"Mohamed124@");

 */