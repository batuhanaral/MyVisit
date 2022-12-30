using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ProjeA.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeA.Models;
using System.IO;
using System.Net.NetworkInformation;
using System.Web.Helpers;
using Microsoft.Owin.Security;
using System.Windows.Controls;


namespace ProjeA.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public AdminController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityContext()));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityContext()));
        }
       
        // GET: Admin
        public ActionResult Index()
        {
            return View(userManager.Users);
        }

        public ActionResult Message()
        {
            DataContext context = new DataContext();
            var mem = context.Members.ToList();
            return View(mem);
        }

       
        public ActionResult Answer(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }
        [HttpPost]
        public ActionResult Answer(string Email,string message)
        {
            DataContext db = new DataContext();
            var member = new Contact.Member();
            var user = db.Members.Where(x => x.Email == Email).ToList().LastOrDefault();
            string subject = "MYVİSİT TEKNİK DESTEK";
            string body = message;
            WebMail.Send(Email, subject, body, null, null, null, true, null, null, null, null, null, null);
            ViewBag.Mesaj = "Mail gönderimi başarılı";
            user.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Message");
            
        }

        public ActionResult Message2()
        {
            DataContext context = new DataContext();
            var foreings = context.Foreings.ToList();
            return View(foreings);
        }

        public ActionResult Answer2(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }
        [HttpPost]
        public ActionResult Answer2(string Email, string message)
        {
            DataContext db = new DataContext();
            var foreing = new Contact.Foreing();
            var user = db.Foreings.Where(x => x.Email == Email).ToList().LastOrDefault(); ;
            string subject = "MYVİSİT TEKNİK DESTEK";
            string body = message;
            WebMail.Send(Email, subject, body, null, null, null, true, null, null, null, null, null, null);
            ViewBag.Mesaj = "Mail gönderimi başarılı";
            user.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Message2");

        }
        [HttpGet]
        public ActionResult AdminRemove(string id)
        {
            var kullanici = userManager.FindById(id);
            if (kullanici != null)
            {
                userManager.Delete(kullanici);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RegisterAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterAdmin(Register model)
        {


            IdentityContext db = new IdentityContext();
            var user = new ApplicationUser();
            var kullanici = db.Users.Where(x => x.UserName == model.Email).ToList();


            if (kullanici.Count() == 0)
            {
                user.Name = model.Name;
                user.Surname = model.SurName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.MembershipStatus = true;
                IdentityResult result = userManager.Create(user, model.Password);
                if (result.Succeeded)
                {

                    //kullanıcı oluşunca role atıyoruz!!!


                    userManager.AddToRole(user.Id, "admin");


                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("RegisterUserError", "Kullanıcı oluşturma hatası");
                }
            }
            else
            {
                ModelState.AddModelError("", "Bu Email kullanılmaktadır ");
            }

            return View();
        }

        public ActionResult YoneticiGiris()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YoneticiGiris(Login model)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    if (userManager.IsInRole(user.Id, "admin"))
                    {
                        var authManager = HttpContext.GetOwinContext().Authentication;// kullanıcı girdi çıktılarını yönetmek için
                        var identityclaims = userManager.CreateIdentity(user, "ApplicationCookie"); // kullanıcı için cookie oluşturmak için
                        var authProperties = new AuthenticationProperties();
                        authProperties.IsPersistent = model.RememberMe;//hatırlamak için
                        authManager.SignOut();
                        authManager.SignIn(authProperties, identityclaims);
                        return RedirectToAction("Index", "Admin");

                    }
                    else
                    {
                        ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz...");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Giriş bilgilerinizi kontrol ediniz...");
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

    }

}