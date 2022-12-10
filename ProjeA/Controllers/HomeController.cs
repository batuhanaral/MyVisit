using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ProjeA.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeA.Models;
using System.Web.Helpers;
using Microsoft.Owin.Security;

namespace ProjeA.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public HomeController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityContext()));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityContext()));
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.Surname = model.SurName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.UserName = model.Email;
                IdentityResult result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {

                    //kullanıcı oluşunca role atıyoruz!!!
                  
                    
                    userManager.AddToRole(user.Id, "pesonal");
                    

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturma hatası");
                }
            }
            return View();
        }




        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                //Giriş işlemleri
                var user = userManager.Find(model.UserName, model.Password);
                

                if (user != null)
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;
                    var identityclaims = userManager.CreateIdentity(user, "ApplicationCookie");
                    var authProperties = new AuthenticationProperties();
                    
                    authManager.SignIn(authProperties, identityclaims);
                    return RedirectToAction("Index2", "Home");
                }
                else
                {
                    ModelState.AddModelError("LoginUserError", "Kullanıcı oluşturama hatası");
                }


            }
            return View(model);
        }
        public ActionResult Logout()
        {

            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");

        }


        public ActionResult PasswordUpdate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PasswordUpdate(Register model)
        {
            string id = User.Identity.GetUserId();
            var user = userManager.Users.FirstOrDefault(x => x.Id == id);

            //user.PasswordHash = model.Password;
            user.PasswordHash = userManager.PasswordHasher.HashPassword(model.Password);
            userManager.Update(user);
            return RedirectToAction("Index2", "Home");
        }
    }
}