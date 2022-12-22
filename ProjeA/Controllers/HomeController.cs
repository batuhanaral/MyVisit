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
using System.Reflection;
using System.IO;
using System.Net;

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
                IdentityContext db = new IdentityContext();
                var user = new ApplicationUser();
                var kullanici = db.Users.Where(x => x.UserName == model.Email).ToList();

                user.Name = model.Name;
                user.Surname = model.SurName;
                user.PhoneNumber = model.PhoneNumber;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.MembershipStatus = true;
                if (kullanici.Count() == 0)
                {
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
                else
                {
                    ModelState.AddModelError("", "Bu Email kullanılmaktadır ");
                }

            }
            return View();
        }




        public ActionResult Index2()
        {
            string id = User.Identity.GetUserId();
            var user = userManager.Users.FirstOrDefault(x => x.Id == id);

            ViewBag.mesaj = user.Name + " " + user.Surname;
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
                    ViewBag.ErrorMessage = "Giriş Bilgileriniz yanlıştır.";
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


        public ActionResult MyVisitEdit()
        {
            DataContext db = new DataContext();
            string username = User.Identity.GetUserName();
            //int num = int.Parse(id);

            var user = db.Informations.FirstOrDefault(x => x.Email == username);
            if (user == null)
            {
                return View();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult MyVisitEdit(Information model, string photo)
        {

            DataContext db2 = new DataContext();
            /*username bulmak için*/
            string id = User.Identity.GetUserId();
            var username = userManager.Users.FirstOrDefault(x => x.Id == id);
            var user = db2.Informations.Where(x => x.Email == model.Email);
            /*-------------------*/
            var kullanici = new Information();
            if (user.Count() == 0)
            {
                kullanici.WebSite = model.WebSite;
                kullanici.Name = model.Name;
                kullanici.Surname = model.Surname;
                kullanici.Number = model.Number;
                kullanici.Email = model.Email;
                kullanici.Adress = model.Adress;
                kullanici.Iban = model.Iban;
                kullanici.LinkedIn = model.LinkedIn;
                kullanici.Company = model.Company;
                kullanici.Title = model.Title;
                kullanici.Instagram = model.Instagram;
                kullanici.Facebook = model.Facebook;
                kullanici.Tiktok = model.Tiktok;
                if (Request.Files.Count != 0)
                {
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);

                    string yol = "~/LogoDepo/" + dosyaAdi;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    kullanici.Photo = "/PdfDepo/" + username.UserName;
                }
                kullanici.User_Id = id;
                db2.Informations.Add(kullanici);
                db2.SaveChanges();

            }
            else
            {
                kullanici.WebSite = model.WebSite;
                kullanici.Name = model.Name;
                kullanici.Surname = model.Surname;
                kullanici.Number = model.Number;
                kullanici.Email = model.Email;
                kullanici.Adress = model.Adress;
                kullanici.Iban = model.Iban;
                kullanici.LinkedIn = model.LinkedIn;
                kullanici.Company = model.Company;
                kullanici.Title = model.Title;
                kullanici.Instagram = model.Instagram;
                kullanici.Facebook = model.Facebook;
                kullanici.Tiktok = model.Tiktok;
                if (Request.Files.Count != 0)
                {
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);

                    
                    if (dosyaAdi==null)
                    {
                        string yol = "~/LogoDepo/" + dosyaAdi;
                        Request.Files[0].SaveAs(Server.MapPath(yol));
                        kullanici.Photo = "/PdfDepo/" + username.UserName;
                    }
                    
                    
                }               

                kullanici.User_Id = id;
                db2.SaveChanges();
            }

            return View();
        }
        //[HttpPost]
        //public ActionResult MyVisitEdit( ViewModel model)
        //{

        //}


        public ActionResult aaaa()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact.Foreing model, string message)
        {
            DataContext db = new DataContext();
            Contact.Foreing foreing = new Contact.Foreing();
            foreing.Name = model.Name;
            foreing.Email = model.Email;
            foreing.Phone = model.Phone;
            foreing.Message = message;
            foreing.Durum = true;
            db.Foreings.Add(foreing);
            db.SaveChanges();
            if (ModelState.IsValid)
            {
                // Veri ekleme işlemleri

                TempData["Success"] = "Mesaj başarıyla gönderildi.";
                return RedirectToAction("Contact2");
            }
            return View();
        }


        public ActionResult Contact2()
        {
            string id = User.Identity.GetUserId();
            var username = userManager.Users.FirstOrDefault(x => x.Id == id);

            return View(username);
        }
        [HttpPost]
        public ActionResult Contact2(ApplicationUser model, string message)
        {
            DataContext db = new DataContext();
            Contact.Member member = new Contact.Member();
            member.Name = model.Name;
            member.Email = model.Email;
            member.Phone = model.PhoneNumber;
            member.Message = message;
            member.Durum = true;
            db.Members.Add(member);
            db.SaveChanges();
            if (ModelState.IsValid)
            {
                // Veri ekleme işlemleri

                TempData["Success"] = "Mesaj başarıyla gönderildi.";
                return RedirectToAction("Contact2");
            }

            return View(model);
        }
    }
}