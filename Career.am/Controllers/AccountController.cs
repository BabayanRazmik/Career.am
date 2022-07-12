using DataAccess.DAL.Interfaces;
using DataAccess.Models;
using DataModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Career.am.Controllers
{
    public class AccountController : BaseController
    {
        #region BaseController

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
            : base(userManager, signInManager, unitOfWork)
        {

        }

        #endregion

        #region Registration
        [HttpGet]
        public IActionResult Registration()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel Register)
        {

            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = Register.FirstName;
                user.LastName = Register.LastName;
                user.UserName = Register.Email;
                user.Email = Register.Email;
                user.PasswordHash = Register.Password;

                var addUser = await _userManager.CreateAsync(user, Register.Password);

                if (addUser.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("ListActiveJob", "Job");
                }
                else
                {
                    foreach (var error in addUser.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(Register);
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel log)
        {
            if (ModelState.IsValid )
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(log.Email, log.Password, log.RemmemberMe, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListActiveJob", "Job");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Incorrect Email or Password");
                return View(log);
            }
        }
        #endregion

        #region Log out

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
