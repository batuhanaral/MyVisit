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
using System.Web.UI.WebControls;

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
            var user = db.Members.FirstOrDefault(x => x.Email == Email);
            string subject = "MYVİSİT TEKNİK DESTEK";
            string body = message;
            WebMail.Send(Email, subject, body, null, null, null, true, null, null, null, null, null, null);
            ViewBag.Mesaj = "Mail gönderimi başarılı";
            user.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Message");
            
        }




    }
}