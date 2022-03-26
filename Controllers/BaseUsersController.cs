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
    public class BaseUsersController : Controller
    {
        private readonly UserManager<BaseUser> _baseUserManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public BaseUsersController(UserManager<BaseUser> baseUserManager, UserManager<ApplicationUser> userManager)
        {
            _baseUserManager = baseUserManager;
            _userManager = userManager;
        }
        public  IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> BaseUsersForm(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return View("AddBaseUsersForm", new AddUserFormViewModel());

            var baseUser = await _baseUserManager.FindByIdAsync(id);

            if (baseUser == null)
                return NotFound();
            var adminViewModel = new EditUserFormViewModel
            {
                Email = baseUser.Email,
                FirstName = baseUser.FirstName,
                LastName = baseUser.LastName,
                PhoneNumber = baseUser.PhoneNumber,
                UserName = baseUser.UserName,
            };
            return View("EditBaseUsersForm", adminViewModel);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBaseUser(AddUserFormViewModel userFormViewModel)
        {
            if (!ModelState.IsValid)
                return View("AddBaseUsersForm", userFormViewModel);

            //Valid Email
            var email = await _userManager.FindByEmailAsync(userFormViewModel.Email);

            if (email != null)
            {
                ModelState.AddModelError("Email", "This email is already exists!");
                return View("AddBaseUsersForm", userFormViewModel);
            }

            //Valid Username
            var userName = await _userManager.FindByNameAsync(userFormViewModel.UserName);

            if (userName != null)
            {
                ModelState.AddModelError("UserName", "This username is already exists!");
                return View("AddBaseUsersForm", userFormViewModel);
            }

            var baseUser = new BaseUser
            {
                Email = userFormViewModel.Email,
                FirstName = userFormViewModel.FirstName,
                LastName = userFormViewModel.LastName,
                PhoneNumber = userFormViewModel.PhoneNumber,
                UserName = userFormViewModel.UserName
            };
            var result = await _baseUserManager.CreateAsync(baseUser, userFormViewModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("PhoneNumber", error.Description);
                    return View("AddBaseUsersForm", userFormViewModel);
                }
            }

            await _baseUserManager.AddToRoleAsync(baseUser, RoleName.User);

            return RedirectToAction(nameof(Index));
        }





        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBaseUser(EditUserFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View("EditBaseUsersForm", viewModel);

            if (string.IsNullOrWhiteSpace(viewModel.Id))
                return BadRequest();

            var baseUser = await _baseUserManager.FindByIdAsync(viewModel.Id);

            if (baseUser == null)
                return NotFound();

            var userByEmail = await _userManager.FindByEmailAsync(viewModel.Email);

            if (userByEmail != null && userByEmail.Id != viewModel.Id)
            {
                ModelState.AddModelError("Email", "This email is already exist!");
                return View("EditBaseUsersForm", viewModel);
            }
            var userByUsername = await _userManager.FindByNameAsync(viewModel.UserName);
            if (userByUsername != null && userByUsername.Id != viewModel.Id)
            {
                ModelState.AddModelError("UserName", "This username is already exist!");
                return View("EditBaseUsersForm", viewModel);
            }


            //Map data
            baseUser.Email = viewModel.Email;
            baseUser.UserName = viewModel.UserName;
            baseUser.FirstName = viewModel.FirstName;
            baseUser.LastName = viewModel.LastName;
            baseUser.PhoneNumber = viewModel.PhoneNumber;

            await _baseUserManager.UpdateAsync(baseUser);

            return RedirectToAction(nameof(Index));

        }


    }
}
