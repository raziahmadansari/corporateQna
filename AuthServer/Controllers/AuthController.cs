using AuthServer.Models;
using AuthServer.ViewModels;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly IIdentityServerInteractionService InteractionService;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IIdentityServerInteractionService interactionService)
        {
            SignInManager = signInManager;
            UserManager = userManager;
            InteractionService = interactionService;
        }

        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // this will signout the user from identity server
            await SignInManager.SignOutAsync();
            var logoutRequest = await InteractionService.GetLogoutContextAsync(logoutId);
            if (string.IsNullOrEmpty(logoutRequest.PostLogoutRedirectUri))
            {
                // return RedirectToAction("Index", "Home");
                return Redirect("http://localhost/4200");
            }
            return Redirect(logoutRequest.PostLogoutRedirectUri);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            var result = await SignInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            if (result.Succeeded)
            {
                return Redirect(user.ReturnUrl);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel userDetails)
        {
            if (!ModelState.IsValid)
            {
                return View(userDetails);
            }

            var user = new ApplicationUser
            {
                UserName = userDetails.UserName,
                Name = userDetails.Name,
                Designation = userDetails.Designation,
                Team = userDetails.Team,
                Category = userDetails.Category,
                Location = userDetails.Location
            };

            var result = await UserManager.CreateAsync(user, userDetails.Password);

            if (result.Succeeded)
            {
                await SignInManager.SignInAsync(user, false);
                return Redirect(userDetails.ReturnUrl);
            }

            return View();
        }
    }
}
