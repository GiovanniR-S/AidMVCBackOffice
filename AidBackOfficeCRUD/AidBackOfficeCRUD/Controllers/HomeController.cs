﻿using AidBackOfficeCRUD.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace AidBackOfficeCRUD.Controllers {

    public class HomeController:Controller {

        private readonly UserManager<MyUser> _userManager;

        public HomeController (UserManager<MyUser> userManager) {
            _userManager = userManager;
        }

        public IActionResult Index () {
            return View();
        }

        public IActionResult Privacy () {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginModel model) {

            if(ModelState.IsValid) {

                var user = await _userManager.FindByNameAsync(model.UserName);

                if(user != null && await _userManager.CheckPasswordAsync(user, model.Password)) {

                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return RedirectToAction("About");

                }

                ModelState.AddModelError("", "Usuário ou senha inválido");

            }

            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Login () {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model) {

            if(ModelState.IsValid) {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null) {

                    user = new MyUser() {
                        UserName = model.UserName,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                }

                return View("Success");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register () { 
        
            return View();
        
        }

        [HttpGet]
        public IActionResult Success () {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult About () {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error () {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}