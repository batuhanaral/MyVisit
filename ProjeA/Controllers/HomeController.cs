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
using static System.Net.WebRequestMethods;
using static QRCoder.PayloadGenerator;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net.NetworkInformation;
using System.Reflection.Emit;


namespace ProjeA.Controllers
{
    
    public class HomeController : Controller
    {
        public UserManager<ApplicationUser> userManager;
        public RoleManager<IdentityRole> roleManager;
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


        public string ka;

        public ActionResult Index2()
        {
            string id = User.Identity.GetUserId();
            var user = userManager.Users.FirstOrDefault(x => x.Id == id);
            ViewBag.LayoutId = user.UserName;   
            ViewBag.mesaj = user.Name + " " + user.Surname;
            ka= user.UserName;
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
            string id = User.Identity.GetUserId();
            var user = userManager.Users.FirstOrDefault(x => x.Id == id);
            ViewBag.LayoutId = user.UserName;
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
            string id2 = User.Identity.GetUserId();
            var user2 = userManager.Users.FirstOrDefault(x => x.Id == id2);
            ViewBag.LayoutId = user2.UserName;
            //int num = int.Parse(id);

            var user = db.Informations.FirstOrDefault(x => x.Email == username);
            if (user == null)
            {
                return View();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult MyVisitEdit(Information model, HttpPostedFileBase photo, QRCodeModel qRCode)
        {

            DataContext db2 = new DataContext();
            /*username bulmak için*/
            string id = User.Identity.GetUserId();
            var username = userManager.Users.FirstOrDefault(x => x.Id == id);
            var user = db2.Informations.Where(x => x.Email == model.Email);
            var kullanici2 = db2.Informations.FirstOrDefault(x => x.Email == username.UserName);

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
                /*----------------------*/

                kullanici.WebSiteDurum = "~/LogoDepo/web.png";
                
                kullanici.NumberDurum = "~/LogoDepo/number.png";
                kullanici.EmailDurum = "~/LogoDepo/email.png";
                kullanici.AdressDurum = "~/LogoDepo/adress.png";
                
                kullanici.LinkedInDurum = "~/LogoDepo/linkedin.png";
                kullanici.InstagramDurum = "~/LogoDepo/instagram.png";
                kullanici.FacebookDurum = "~/LogoDepo/facebook.png";
                kullanici.TiktokDurum = "~/LogoDepo/tiktok.png";
                /*----------------------*/
                if (Request.Files.Count != 0)
                {
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);

                    string yol = "~/LogoDepo/" + username.Email + ".png";
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    kullanici.Photo = username.UserName + ".png";
                    //photo.SaveAs(Server.MapPath(yol));  
                }
                string QRCodeText = "https://localhost:44301/Home/aaaa?email=" + model.Email;
                QRCodeGenerator QrGenerator = new QRCodeGenerator();
                QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(QRCodeText, QRCodeGenerator.ECCLevel.Q);
                QRCode QrCode = new QRCode(QrCodeInfo);
                Bitmap QrBitmap = QrCode.GetGraphic(60);
                byte[] BitmapArray = QrBitmap.BitmapToByteArray();
                //string BitmapArray2 = model.Email;

                string QrUri = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));
                ViewBag.QrCodeUri = QrUri;

                byte[] imageData = Convert.FromBase64String(QrUri.Split(',')[1]);
                string fileName =  model.Email +"Qr"+".png";
                string filePath = Path.Combine(Server.MapPath("~/LogoDepo"), fileName);
                using (FileStream fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    fs.Write(imageData, 0, imageData.Length);
                }
                kullanici.QrCode = fileName;
                kullanici.User_Id = id;
                db2.Informations.Add(kullanici);
                db2.SaveChanges();
                /*qr code*/
                //using (MemoryStream ms =new MemoryStream())
                //{
                //    QRCode qRCode1=new QRCode();
                //    string QRCodeText = "https://localhost:44301/Home/aaaa?email=" + model.Email;
                //    QRCodeGenerator QrGenerator = new QRCodeGenerator();
                //    QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(QRCodeText, QRCodeGenerator.ECCLevel.Q);
                //    using (Bitmap bitmap)
                //    {

                //    }
                //}

                

                return View();

            }

            else
            {
                kullanici2.WebSite = model.WebSite;
                kullanici2.Name = model.Name;
                kullanici2.Surname = model.Surname;
                kullanici2.Number = model.Number;
                kullanici2.Email = model.Email;
                kullanici2.Adress = model.Adress;
                kullanici2.Iban = model.Iban;
                kullanici2.LinkedIn = model.LinkedIn;
                kullanici2.Company = model.Company;
                kullanici2.Title = model.Title;
                kullanici2.Instagram = model.Instagram;
                kullanici2.Facebook = model.Facebook;
                kullanici2.Tiktok = model.Tiktok;
                /*----------------------*/

                
                /*----------------------*/
                if (Request.Files.Count != 0)
                {
                    string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);

                    
                    if (dosyaAdi!=null)
                    {
                        string yol = "~/LogoDepo/" + username.Email+".png";
                        Request.Files[0].SaveAs(Server.MapPath(yol));
                        kullanici.Photo =  yol;
                        //photo.SaveAs(Server.MapPath(yol));
                    }
                    
                    
                }               

                kullanici2.User_Id = id;
                db2.SaveChanges();
            }

            return View();
        }
        //[HttpPost]
        //public ActionResult MyVisitEdit( ViewModel model)
        //{

        //}


        public ActionResult aaaa(string email)
        {
            DataContext db = new DataContext();
            string username = User.Identity.GetUserName();
            //int num = int.Parse(id);
            
            var user = db.Informations.FirstOrDefault(x => x.Email == username);
            if (user == null)
            {
                return View();
            }
            string privatePageUrl = "https://localhost:44301/Home/aaaa?email=" + email;
            ViewBag.Link = privatePageUrl;
            return View(user);
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
                return RedirectToAction("Contact");
            }
            return View();
        }


        public ActionResult Contact2()
        {
            string id = User.Identity.GetUserId();
            var username = userManager.Users.FirstOrDefault(x => x.Id == id);
            
            ViewBag.LayoutId = username.UserName;
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

    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }

}