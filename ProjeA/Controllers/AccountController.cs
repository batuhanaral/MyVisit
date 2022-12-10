using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using ProjeA.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeA.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public AccountController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityContext()));
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityContext()));
        }

       
        
    }
}