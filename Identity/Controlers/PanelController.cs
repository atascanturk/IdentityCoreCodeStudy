using Identity.Context;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controlers
{
    [Authorize]
    public class PanelController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public PanelController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task <IActionResult> Index()
        {
          var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(user);
        }

        public async Task<IActionResult> UpdateUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserUpdateViewModel model = new UserUpdateViewModel
            {
                Email = user.Email, PhoneNumber= user.PhoneNumber , Name = user.Name, SurName=user.SurName, PictureUrl=user.PictureUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateViewModel model) 
        {
            if (ModelState.IsValid) 
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                if(model.Picture!= null) 
                {
                    var uygulamaninCalistigiYer = Directory.GetCurrentDirectory();
                    var uzanti =  Path.GetExtension( model.Picture.FileName);
                    var resimAd = Guid.NewGuid() + uzanti;
                    var kaydedilecekYer = uygulamaninCalistigiYer + "/wwwroot/img/" + resimAd;
                    
                   using var stream = new FileStream(kaydedilecekYer, FileMode.Create);

                   await  model.Picture.CopyToAsync(stream);
                    user.PictureUrl = resimAd;
                }
                user.Name = model.Name;
                user.SurName = model.SurName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await _userManager.UpdateAsync(user);
                
                if (result.Succeeded) 
                {
                    return RedirectToAction("Index");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }

            return View(model);
        }
        public async Task<IActionResult> LogOut() 
        {
           await _signInManager.SignOutAsync();

            return RedirectToAction("index", "Home");
        }

    }
}
