namespace Semillitas.Web.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Semillitas.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Semillitas.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            
            //var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            // Creating all roles
            if (!roleManager.RoleExists(RoleNames.ROLE_ADMINISTRATOR))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_ADMINISTRATOR));
            }
            if (!roleManager.RoleExists(RoleNames.ROLE_OPERATOR))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_OPERATOR));
            }
            if (!roleManager.RoleExists(RoleNames.ROLE_USER))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleNames.ROLE_USER));
            }
            //context.SaveChanges();
            
            // Creating users
            if (userManager.FindByName("admin@semillitas.com") == null) {
                var user = new ApplicationUser()
                {
                    UserName = "admin@semillitas.com",
                    FirstName = "Admin",
                    Email = "admin@semillitas.com"
                };
                var userResult = userManager.Create(user, string.Format("admin12345"));
                if (userResult.Succeeded)
                    userManager.AddToRole(user.Id, RoleNames.ROLE_ADMINISTRATOR);
            }

            if (userManager.FindByName("roberto.chino@gmail.com") == null)
            {
                var user = new ApplicationUser()
                {
                    UserName = "roberto.chino@gmail.com",
                    FirstName = "Roberto",
                    Email = "roberto.chino@gmail.com"
                };
                var userResult = userManager.Create(user, string.Format("admin12345"));
                if (userResult.Succeeded)
                    userManager.AddToRole(user.Id, RoleNames.ROLE_USER);
            }

            // p.Name indicates the variable that should be unique (if already exists, do not create)
            context.Memberships.AddOrUpdate(
                  p => p.Name,
                  new Membership { Code = MembershipCodes.MEMBERSHIP_ANNUAL, Name = "Annual", Price = 500, Inscription = 100, DurationInDays = 365, CreationDate = DateTime.Now, CreationUserName = "Admin", ModifDate = DateTime.Now, ModifUserName = "Admin" },
                  new Membership { Code = MembershipCodes.MEMBERSHIP_MONTHLY, Name = "Monthly", Price = 150, Inscription = 25, DurationInDays = 30, CreationDate = DateTime.Now, CreationUserName = "Admin", ModifDate = DateTime.Now, ModifUserName = "Admin" },
                  new Membership { Code = MembershipCodes.MEMBERSHIP_DAILY, Name = "Daily", Price = 10, Inscription = 0, DurationInDays = 1, CreationDate = DateTime.Now, CreationUserName = "Admin", ModifDate = DateTime.Now, ModifUserName = "Admin" }
                );

            

        }
    }
}
