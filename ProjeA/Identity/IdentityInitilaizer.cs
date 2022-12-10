using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjeA.Identity
{
    public class IdentityInitilaizer : DropCreateDatabaseIfModelChanges<IdentityContext>
    {
        protected override void Seed(IdentityContext context)
        {

            if (!context.Roles.Any(i => i.Name == "admin"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "admin", Description = "admin rol" };
                manager.Create(role);
            }

            //Roller
            if (!context.Roles.Any(i => i.Name == "personal"))
            {
                var store = new RoleStore<ApplicationRole>(context);
                var manager = new RoleManager<ApplicationRole>(store);
                var role = new ApplicationRole() { Name = "pesonal", Description = "personal rol" };
                manager.Create(role);
            }

            base.Seed(context); 
        }
    }
}