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
    public class SellersController : Controller
    {

        private readonly UserManager<Seller> _sellerManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SellersController(UserManager<Seller> sellerManager, UserManager<ApplicationUser> userManager)
        {
            _sellerManager = sellerManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SellersForm(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return View("AddSellersForm", new AddUserFormViewModel());

            var seller = await _sellerManager.FindByIdAsync(id);

            if (seller == null)
                return NotFound();
            var adminViewModel = new EditUserFormViewModel
            {
                Email = seller.Email,
                FirstName = seller.FirstName,
                LastName = seller.LastName,
                PhoneNumber = seller.PhoneNumber,
                UserName = seller.UserName,
            };
            return View("EditSellersForm", adminViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSeller(AddUserFormViewModel userFormViewModel)
        {
            if (!ModelState.IsValid)
                return View("AddSellersForm", userFormViewModel);

            //Valid Email
            var email = await _userManager.FindByEmailAsync(userFormViewModel.Email);

            if (email != null)
            {
                ModelState.AddModelError("Email", "This email is already exists!");
                return View("AddSellersForm", userFormViewModel);
            }

            //Valid Username
            var userName = await _userManager.FindByNameAsync(userFormViewModel.UserName);

            if (userName != null)
            {
                ModelState.AddModelError("UserName", "This username is already exists!");
                return View("AddSellersForm", userFormViewModel);
            }

            var seller = new Seller
            {
                Email = userFormViewModel.Email,
                FirstName = userFormViewModel.FirstName,
                LastName = userFormViewModel.LastName,
                PhoneNumber = userFormViewModel.PhoneNumber,
                UserName = userFormViewModel.UserName
            };
            var result = await _sellerManager.CreateAsync(seller, userFormViewModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("PhoneNumber", error.Description);
                    return View("AddSellersForm", userFormViewModel);
                }
            }

            await _sellerManager.AddToRoleAsync(seller, RoleName.Seller);

            return RedirectToAction(nameof(Index));
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSeller(EditUserFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("EditSellersForm", viewModel);

            if (string.IsNullOrWhiteSpace(viewModel.Id))
                return BadRequest();

            var admin = await _sellerManager.FindByIdAsync(viewModel.Id);

            if (admin == null)
                return NotFound();

            var userByEmail = await _userManager.FindByEmailAsync(viewModel.Email);

            if (userByEmail != null && userByEmail.Id != viewModel.Id)
            {
                ModelState.AddModelError("Email", "This email is already exist!");
                return View("EditSellersForm", viewModel);
            }
            var userByUsername = await _userManager.FindByNameAsync(viewModel.UserName);
            if (userByUsername != null && userByUsername.Id != viewModel.Id)
            {
                ModelState.AddModelError("UserName", "This username is already exist!");
                return View("EditSellersForm", viewModel);
            }


            //Map data
            admin.Email = viewModel.Email;
            admin.UserName = viewModel.UserName;
            admin.FirstName = viewModel.FirstName;
            admin.LastName = viewModel.LastName;
            admin.PhoneNumber = viewModel.PhoneNumber;

            await _sellerManager.UpdateAsync(admin);

            return RedirectToAction(nameof(Index));

        }
    }
}
