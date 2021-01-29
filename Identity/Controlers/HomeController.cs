using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controlers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        

        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View(new UserSignInViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToke n]
        public async Task<IActionResult> GirisYap(UserSignInViewModel model)
        {
            if (ModelState.IsValid)
            {
               var identityresult = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, true);


                //if (identityresult.IsNotAllowed) 
                //{
                //    ModelState.AddModelError("", "Lütfen email adresinizi doğrulayınız.");
                //    return View("Index", model);
                //}

               if( identityresult.Succeeded)
                {
                    return RedirectToAction("Index", "Panel");
                }

                if (identityresult.IsLockedOut)
                {
                    var gelen = await _userManager.GetLockoutEndDateAsync(await _userManager.FindByNameAsync(model.UserName));
                    var sure = gelen.Value;
                   var kalanDakika = sure.Minute - DateTime.Now.Minute  ;
                    ModelState.AddModelError("", $"Üst üste hatalı deneme, {kalanDakika} dakika boyunca hesabınız kitlendi");
                    return View("Index", model);
                }

                
                var yanlissayisi = await _userManager.GetAccessFailedCountAsync(await _userManager.FindByNameAsync(model.UserName));
        
                

                ModelState.AddModelError("", $"Kullanıcı adı veya şifre hatalı, { 5 - yanlissayisi} hakkınız kaldı.");
            }
           
            return View("Index",model);
        }

        public IActionResult KayitOl()
        {
            return View(new UserSignUpVİewModel());
        }
        [HttpPost]
        public async Task<IActionResult> KayitOl(UserSignUpVİewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    Email = model.Email,
                    Name = model.Name,
                    SurName = model.SurName,
                    UserName = model.UserName

                };
              var result= await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                    }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        public IActionResult AccessDenied() 
        {
            return View();
        }
    }
}
